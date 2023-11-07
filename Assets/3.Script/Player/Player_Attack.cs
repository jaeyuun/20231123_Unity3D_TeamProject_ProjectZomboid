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
    [Header("건사운드오브젝트")]
    public GameObject Sound_Gun;
    private bool IsMovement=false;


    [Header("엑티브할 베트들")]
    //등에 있는 배트
    [SerializeField] private GameObject Bat_Spine;
    private bool Bat_In=true;    
    //손에 있는 배트
    [SerializeField] private GameObject Bat_Hand;
    private bool Bat_Out;


    [Header("테스트를위한")]
    public bool Bat_Get;//배트를 가지고 있냐?    
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
        if (Input.GetButtonDown("Jump")&&!IsMovement)//동작하길 바란다...Todo 필요없다면 삭제 하길...
        {
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("isKickig");
            IsMovement = true;
            Invoke("isMovement", 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !IsMovement)
        {
            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생
            Debug.Log("누르는중~~~");
            Bat_take_out(Bat_Get);

        }

        if (Input.GetMouseButton(1))//우클릭시
        {

            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생
            if (Melee_weapon)//근접무기를 들고 있다면
            {
                anim.SetBool("isWeapon", true);//근접무기 대기자세를 재생하고
                if (Input.GetMouseButtonDown(0) && !IsMovement)//좌클릭을 하면
                {
                    anim.SetTrigger("isSwing");//스윙공격을 한다
                    isAttack = true;
                    Invoke("BatSWingClip", 0.3f);
                    IsMovement = true;
                    Invoke("isMovement", 1f);
                }
                
            }
            else if (Range_weapon)
            {
                anim.SetBool("isAiming", true);
                if (Input.GetMouseButton(0) && !IsMovement)//총쏘기
                {

                    gun_Shot.ShotEvent();
                    anim.SetTrigger("isFiring");
                    Sound_Gun.SetActive(false);
                    IsMovement = true;
                    Invoke("isMovement", 0.3f);
                }
            }
   
        }
        else if(Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isWeapon", false);
            anim.SetBool("isAiming", false);
            anim.SetLayerWeight(1, 0);
        }
    }

    private void FixedUpdate()
    {
        if (Range_weapon==true)
        {
            anim.SetLayerWeight(1, 1);//상체 애니메이션 재생
            anim.SetBool("isGun", true);
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
            Debug.Log("배트는있어");
            if (Bat_In)
            {
                Debug.Log("등뒤에 있어");
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Spine, Bat_Hand, false, true));
            }
            else if (Bat_Out)
            {
                anim.SetTrigger("isOver");
                StartCoroutine(ActivateWithDelay(Bat_Hand, Bat_Spine, true, false));
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

    private void isMovement()//행동가능여부
    {
        IsMovement = false;
    }


}
