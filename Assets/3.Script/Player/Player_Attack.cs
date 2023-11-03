using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    public bool isAttack;
    public AudioClip BatSwing;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            anim.SetLayerWeight(1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("isSwing");
                isAttack = true;
                Invoke("BatSWingClip", 0.3f);
            }
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    private void BatSWingClip()
    {
        audioSource.PlayOneShot(BatSwing);

    }
}
