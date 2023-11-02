using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    // Zombie NavMesh
    private NavMeshAgent nav;
    private GameObject player; // 플레이어의 현재 위치
    private Animator zombieAnim;
    private int animSelect = 0;
    private bool isPlayer = false; // 플레이어 소리내는 범위에 trigger 되었을 때 true

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        player = GameObject.FindGameObjectWithTag("Player");

        nav.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(ZombieAnim_Co());
    }

    private void FixedUpdate()
    {
        ZombieTransToPlayer();
    }

    private void ZombieTransToPlayer()
    { // Zombie가 플레이어를 추척하는 메소드
        if (player)
        { // 플레이어가 살아있을 때
            if (isPlayer)
            { // 플레이어 소리 범위 안에 있고
                nav.SetDestination(player.transform.position);
            } else
            { // 플레이어 소리 범위 안에 없고(Hide 시)
              // 좀비 보이는 시야각 넣어주기...
                
            }
        }
    }

    private IEnumerator ZombieAnim_Co()
    {
        yield return new WaitForSeconds(3f);

        animSelect = Random.Range(0, 3);
        if (animSelect.Equals(0))
        { // idle
            zombieAnim.SetBool("isWalk", false);
        }
        else
        { // walk
            zombieAnim.SetBool("isWalk", true);
        }
        StartCoroutine(ZombieAnim_Co());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zombieAnim.SetBool("isPlayerFind", true);
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zombieAnim.SetBool("isPlayerFind", false);
            isPlayer = false;
        }
    }
}
