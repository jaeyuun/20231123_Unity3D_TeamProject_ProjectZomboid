using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 4f;
    Animator animator;
    public GameObject Sound_Walk;
    public GameObject Sound_Run;
    public GameObject Push;
    [SerializeField] private Camera followCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(Input.GetButtonDown("Jump"))//스페이스 누를시에 상대방을 민다.
        {
            animator.SetTrigger("isKickig");
            Push.SetActive(true);
        }
        if (moveHorizontal != 0 || moveVertical != 0)
        {

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1))
            {
                Sound_Walk.SetActive(false);
                Sound_Run.SetActive(true);
                animator.SetBool("isRun", true);
                transform.position += movement * speed * 2f * Time.deltaTime;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
            {
                Sound_Walk.SetActive(false);
                Sound_Walk.SetActive(true);
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", true);
                transform.position += movement * speed * Time.deltaTime;
            }
        }
        if (Input.GetMouseButton(1))//마우스 우클릭
        {
            Rotate(); 
        }
        else if (movement != Vector3.zero) // 마우스를 바라보지 않는 상황에서 이동 중이라면
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }

        else if (moveHorizontal == 0 && moveVertical == 0) // 플레이어 속도가 0이 되었을때
        {
            Sound_Run.SetActive(false);
            Sound_Walk.SetActive(false);
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }
    }

    private void Rotate()
    {

        //마우스 회전
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayhit;
        if (Physics.Raycast(ray, out rayhit, 100))
        {
            Vector3 nextVec = rayhit.point - transform.position;
            // nextVec.x = 0;
            nextVec.y = 0;
            // nextVec.z = 0;
            transform.LookAt(transform.position + nextVec);
        }
    }
    /*void OnCollisionEnter(Collision collision)//방식을 바꿔 볼까 생각중...
    {
        if(collision.gameObject.CompareTag("Zombie"))
        {
            // 충돌한 오브젝트를 가져옵니다
            var otherObject = collision.gameObject;

            // 현재 오브젝트로부터 다른 오브젝트로 향하는 벡터를 가져옵니다
            var direction = otherObject.transform.position - transform.position;

            // 벡터를 뒤집어 다른 오브젝트를 뒤로 미는 방향을 설정합니다
            direction = -direction;

            // 이동할 거리입니다
            float pushBackDistance = 3f;

            // 다른 오브젝트를 뒤로 미릅니다
            otherObject.transform.position += direction.normalized * pushBackDistance;
            Push.SetActive(false);
        }
     
    }*/
}