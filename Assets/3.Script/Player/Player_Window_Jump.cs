using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Window_Jump : MonoBehaviour
{
    [Header("플레이어를 넣으세요")]
    [SerializeField] private Player_Move player;

    private void Start()
    {
        player = GetComponentInParent<Player_Move>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Window"))
        {
            if (Input.GetKeyDown(KeyCode.E))//창문이 열려있는지 확인이 필요
            {
                player.animator.SetTrigger("isClimbing");
                if (player.transform.rotation.y < 0)//WD방향
                {
                    player.transform.position = other.transform.position + new Vector3(-1f, 1f, 1f);
                    Debug.Log("WD방향");
                    Debug.Log(player.transform.rotation.y);
                }
                else if (player.transform.rotation.y > 0)//S방향
                {
                    player.transform.position = other.transform.position + new Vector3(-0.5f, 0, -0.5f);
                    Debug.Log("S방향");
                    Debug.Log(player.transform.rotation.y);
                }
                

            }

        }
    }
}
