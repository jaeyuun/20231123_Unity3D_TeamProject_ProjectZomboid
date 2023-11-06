using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    public bool isAttack;//판정을 위한 bool값
    public AudioClip BatSwing;
    private AudioSource audioSource;

    public bool Melee_weapon;
    public bool Range_weapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {

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
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("탕");
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
}
