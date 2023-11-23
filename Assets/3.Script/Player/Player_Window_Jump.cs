using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Window_Jump : MonoBehaviour
{
    [Header("플레이어를 넣으세요")]
    [SerializeField] private Player_Move player;

    float keydown = 0f;
    private void Start()
    {
        player = GetComponentInParent<Player_Move>();

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Window"))
        {
            if (Input.GetKey(KeyCode.E)) // 창문이 열려있는지 확인이 필요
            {
                keydown += Time.deltaTime;
                WIndow_bool door = other.GetComponent<WIndow_bool>();

                if (door != null) // Door_bool 컴포넌트가 존재한다는 것 확인
                {

                    if (keydown >= 0.8f && door.isOpen)
                    {
                        player.animator.SetTrigger("isClimbing");

                        if (player.transform.rotation.y < 0) //WD방향
                        {
                            player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y - 4f, 0);


                        }
                        else if (player.transform.rotation.y > 0) //S방향
                        {
                            player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y - 4f, 0);


                        }
                        keydown = 0f;
                    }
                }
                else
                {

                }
            }
            else
            {
                keydown = 0f;
            }
        }
        else if (other.gameObject.CompareTag("Fence"))
        {
            if (Input.GetKey(KeyCode.E)) // 창문이 열려있는지 확인이 필요
            {
                player.animator.SetTrigger("isClimbing");

                if (player.transform.rotation.y < 0) //WD방향
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y+2f, 0);


                }
                else if (player.transform.rotation.y > 0) //S방향
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y + 2f, 0);


                }
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                player.animator.SetTrigger("isFence");

                if (player.transform.rotation.y < 0) //WD방향
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y, 0);


                }
                else if (player.transform.rotation.y > 0) //S방향
                {
                    player.transform.position = other.transform.position + new Vector3(0, other.transform.position.y, 0);


                }
            }
        }
    }
}

