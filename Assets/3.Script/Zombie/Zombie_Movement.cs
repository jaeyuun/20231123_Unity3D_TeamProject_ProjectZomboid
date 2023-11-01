using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Movement : MonoBehaviour
{
    private NavMeshAgent nav;
    [SerializeField] private Transform player; // 플레이어의 현재 위치
    private bool isPlayer = false; // 플레이어 소리내는 범위에 trigger 되었을 때 true
    private float moveSpeed = 5;

    private Animator zombieAnim;

    private void Start()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        ZombieTransToPlayer();
    }

    private void ZombieTransToPlayer()
    { // Zombie가 플레이어를 추척하는 메소드
        if (player)
        {
            if (isPlayer)
            { // 플레이어 범위 안에있고, 플레이어 게임 오브젝트가 있을 때
                zombieAnim.SetBool("isPlayerFind", true);
                nav.SetDestination(player.position);
            }
            else
            {
                zombieAnim.SetBool("isPlayerFind", false);
                StartCoroutine(ZombieAnim_Co());
            }
        }
    }

    private IEnumerator ZombieAnim_Co()
    {

        yield return new WaitForSeconds(3f);
        int rand = Random.Range(0, 2);
        Debug.Log(rand);
        if (rand.Equals(0))
        { // idle
            zombieAnim.SetBool("isWalk", false);
        }
        else
        { // walk
            zombieAnim.SetBool("isWalk", true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
