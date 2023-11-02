using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move2 : MonoBehaviour
{
    public float speed;

    float hAxis;
    float vAxis;

    bool wDown;

    Vector3 moveVec;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        Turn();
        GetInput();
    }
    
   
    private void Move()
    {

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        /* if (isSwap)
         {
             moveVec = Vector3.zero;
         }*/

        if (wDown)
        {
            transform.position += moveVec * speed * 1.5f * Time.deltaTime;
        }
        else
        {
            transform.position += moveVec * speed * Time.deltaTime;
        }
        //같은의미 삼항연산자 transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        transform.position += moveVec * speed * Time.deltaTime;



        animator.SetBool("isWalik", moveVec != Vector3.zero);
        animator.SetBool("isRun", wDown);


    }
    private void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }
    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Run");
    }
}
