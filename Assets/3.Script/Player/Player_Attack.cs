using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    public bool isAttack;//판정을 위한 bool값
    public AudioClip BatSwing;
    private AudioSource audioSource;
    [SerializeField] private Gun_Shot gun_Shot;
    public GameObject Sound_Gun;

    //등에 있는 배트
    [SerializeField] private GameObject Bat_Spine;
    private bool Bat_In=true;
    
    //손에 있는 배트
    [SerializeField] private GameObject Bat_Hand;
    private bool Bat_Out;


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
        if (Input.GetButtonDown("Jump"))//동작하길 바란다...Todo 필요없다면 삭제 하길...
        {
            anim.SetLayerWeight(1, 0);
            anim.SetTrigger("isKickig");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
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
                if (Input.GetMouseButtonDown(0))//좌클릭을 하면
                {
                    anim.SetTrigger("isSwing");//스윙공격을 한다
                    isAttack = true;
                    Invoke("BatSWingClip", 0.3f);
                }
                
            }
            else if (Range_weapon)
            {
                anim.SetBool("isAiming", true);
                if (Input.GetMouseButtonDown(0))//총쏘기
                {

                    gun_Shot.ShotEvent();
                    anim.SetTrigger("isFiring");
                    Sound_Gun.SetActive(false);
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



}
