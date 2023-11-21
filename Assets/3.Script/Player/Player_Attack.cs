using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [Header("애니메이션 컨트롤러")]
    public Animator anim;
    [Header("공격판정")]
    public bool isAttack;//판정을 위한 bool값
    [Header("스윙소리")]
    public AudioClip BatSwing;
    private AudioSource audioSource;
    [Header("플레이어오브젝트")]
    [SerializeField] private Gun_Shot gun_Shot;
    [Header("사운드 게임오브젝트")]
    public GameObject Sound_Gun;//게임오브젝트
    private bool IsMovement = false;

    [Header("마우스 커서")]
    public Game_Cursor game_Cursor;

    [Header("엑티브할 베트들")]
    //등에 있는 배트
    [SerializeField] private GameObject Bat_Spine;
    private bool Bat_In = true;
    //손에 있는 배트
    [SerializeField] private GameObject Bat_Hand;
    private bool Bat_Out;

    [Header("calf오브젝트 넣어주세요")]
    [SerializeField] private GameObject calf_l;
    [SerializeField] private GameObject calf_r;

    //등에 있는 총
    [SerializeField] private GameObject Gun_Spine;
    private bool Gun_In = true;
    //손에 있는 총
    [SerializeField] private GameObject Gun_Hand;

    [Header("인벤토리")]
    [SerializeField] private Inventory inventory;
    private bool Gun_Out;
    private int bullet = 0;//인벤토리 총알 수
    private int magazine = 0;//탄창 수 


    [Header("테스트를위한")]
    public bool Bat_Get;//배트를 가지고 있냐?    
    public bool Gun_Get;
    public bool Melee_weapon;
    public bool Range_weapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gun_Shot = GetComponent<Gun_Shot>();


    }

    private void Update()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].itemName == "Bullet")
            {
                bullet = inventory.slots[i].itemCount;                
            }
        }


        if (Melee_weapon)
        {
            anim.SetBool("isWeapon", true);
        }
        else if (!Melee_weapon)
        {
            anim.SetBool("isWeapon", false);
        }
        if (Range_weapon && !IsMovement)
        {
            anim.SetBool("isGun", true);

            if (Input.GetKeyDown(KeyCode.R) && !IsMovement)//재장전
            {
                IsMovement = true; //행동제약
                Debug.Log("재장전찰칵찰칵");
                Invoke("isMovement", 1.5f);
                if(bullet>=20)
                {
                    magazine += 20;//20발장전
                    bullet -= 20;//20발감소
                    for (int i = 0; i < inventory.slots.Length; i++)
                    {
                        if (inventory.slots[i].itemName == "Bullet")
                        {
                            inventory.slots[i].SetSlotCount(-magazine);
                        }
                    }
                    
                }
                else if(bullet>0)
                {
                    magazine += bullet;
                    for (int i = 0; i < inventory.slots.Length; i++)
                    {
                        if (inventory.slots[i].itemName == "Bullet")
                        {
                            inventory.slots[i].SetSlotCount(-magazine);
                        }
                    }
                    bullet = 0;
                }
                else
                {
                    //장전못했어 찰칵칼착 소리재생
                }
            }
            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생

        }
        else if (!Range_weapon)
        {

            anim.SetBool("isGun", false);
            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생

        }


        if (Input.GetButtonDown("Jump") && !IsMovement)//동작하길 바란다...Todo 필요없다면 삭제 하길...
        {
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("isKickig");
            IsMovement = true;
            Invoke("isKick", 0.5f);
            Invoke("isMovement", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsMovement)
        {
            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생
            Debug.Log("누르는중~~~");

            if (Bat_Get)
            {
                Bat_take_out(Bat_Get);
            }
            else if (Gun_Get)
            {
                IsMovement = true;
                Gun_take_out(Gun_Get);
                Invoke("isMovement", 1f);
            }


        }

        if (Input.GetMouseButton(1))//우클릭시
        {
            game_Cursor.OnMouseOver();
            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생
            anim.SetBool("isRight_click", true);

            if (Melee_weapon)//밀리가 있던가 둘다 없던가
            {
                if (Input.GetMouseButtonDown(0) && !IsMovement)//좌클릭을 하면
                {
                    anim.SetTrigger("isSwing");//스윙공격을 한다
                    isAttack = true;
                    Invoke("BatSWingClip", 0.3f);
                    IsMovement = true;
                    Invoke("isMovement", 1f);
                }
            }
            if (!Melee_weapon && !Range_weapon)//둘다 없던가
            {
                if (Input.GetMouseButtonDown(0) && !IsMovement)//좌클릭을 하면
                {
                    anim.SetTrigger("isSwing");//밟는다.
                    Invoke("isStomp", 0.5f);
                    isAttack = true;
                    IsMovement = true;
                    Invoke("isMovement", 1.1f);
                }
            }

            else if (Range_weapon)
            {
                anim.SetBool("isAiming", true);//총조준

                if (Input.GetMouseButton(0) && !IsMovement && magazine > 0)//총쏘기
                {

                    gun_Shot.ShotEvent();
                    anim.SetTrigger("isFiring");
                    Sound_Gun.SetActive(true);
                    IsMovement = true;
                    Invoke("isMovement", 0.3f);
                    magazine -= 1;
                    StartCoroutine(Sound_Gun_false_co());
                }
                else if (Input.GetMouseButton(0) && !IsMovement && magazine == 0)
                {
                    IsMovement = true;
                    Invoke("isMovement", 0.3f);
                    //총알 없는소리 찰칵찰칵
                    Debug.Log("찰칵찰칵");
                }

            }

        }
        else if (Input.GetMouseButtonUp(1))
        {
            game_Cursor.OnMouseExit();
            anim.SetBool("isRight_click", false);
            anim.SetBool("isAiming", false);
            anim.SetLayerWeight(1, 0);
        }
    }

    private void BatSWingClip()
    {
        audioSource.PlayOneShot(BatSwing);
    }

    private void Bat_take_out(bool Bat)// 배트를 뽑는 애니메이션
    {
        if (Bat)
        {

            if (Bat_In)
            {

                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Spine, Bat_Hand, false, true));
                Melee_weapon = true;
                Gun_Hand.SetActive(false);
                Range_weapon = false;
            }
            else if (Bat_Out)
            {
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Hand, Bat_Spine, true, false));
                Melee_weapon = false;

            }
        }
    }

    private void Gun_take_out(bool Gun)// 배트를 뽑는 애니메이션
    {
        if (Gun)
        {

            if (Gun_In)
            {

                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Gun_Spine, Gun_Hand, false, true));
                Range_weapon = true;
                Gun_In = false;
                Gun_Out = true;
            }
            else if (Gun_Out)
            {
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Gun_Hand, Gun_Spine, true, false));
                Range_weapon = false;
                Gun_In = true;
                Gun_Out = false;
                Bat_Hand.SetActive(false);
            }
        }
    }

    private IEnumerator ActivateWithDelay(GameObject objectToDisable, GameObject objectToEnable, bool newBatIn, bool newBatOut)
    {
        yield return new WaitForSeconds(0.8f); // 대기 시간을 1초로 설정했습니다. 원하는 시간으로 변경 가능합니다.
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
        Bat_In = newBatIn;
        Bat_Out = newBatOut;
    }
    private IEnumerator Sound_Gun_false_co()
    {
        yield return new WaitForSeconds(1f);
        Sound_Gun.SetActive(false);
    }
    private void isMovement()//행동가능여부
    {
        IsMovement = false;
        if (calf_l == true)
        {
            calf_l.SetActive(false);
        }
        if (calf_r == true)
        {
            calf_r.SetActive(false);
        }

    }

    private void isKick()
    {
        calf_l.SetActive(true);
    }

    private void isStomp()
    {
        calf_r.SetActive(true);
    }

}
