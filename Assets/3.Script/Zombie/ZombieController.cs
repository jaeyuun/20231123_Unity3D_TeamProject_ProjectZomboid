using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    // Zombie NavMesh
    private NavMeshAgent nav;
    public Transform targetPos;
    private Transform randomTarget; // 플레이어 감지하지 않았을 때 위치
    private Transform player; // 플레이어의 위치

    // RandomTarget NavMesh
    private float range = 10f;
    private Vector3 point;

    private Animator zombieAnim;
    [SerializeField] private GameObject screamRange;
    [SerializeField] private bool isScreamZombie = false;

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        randomTarget = GameObject.FindGameObjectWithTag("RandomTarget").transform;

        // isScreamZombie = zombieData; zombie 종류에 따라 불러오기... todo
    }

    private void Start()
    {
        StartCoroutine(RandomTargetPos_Co());
    }

    private void FixedUpdate()
    {
        ZombieTransToPlayer();
    }

    private void ZombieTransToPlayer()
    {
        // target에 따른 네비 적용
        nav.SetDestination(targetPos.position);
    }

    private IEnumerator RandomTargetPos_Co()
    {
        // random target position select
        if (RandomPoint(randomTarget.position, range, out point))
        {
            randomTarget.position = point;
        }
        targetPos = randomTarget;
        if (Vector3.Distance(randomTarget.position, transform.position) <= 1.0f)
        {
            zombieAnim.SetBool("isIdle", true);
        } else
        {
            zombieAnim.SetBool("isIdle", false);
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(RandomTargetPos_Co());
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // 플레이어 범위 밖일 때 랜덤 타겟
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            zombieAnim.SetBool("isPlayerFind", true);
            if (isScreamZombie && Vector3.Distance(player.position, transform.position) > 1.5f)
            {
                StartCoroutine(ZombieScream_Co());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            targetPos = player;
            if (Vector3.Distance(player.position, transform.position) <= 1.5f)
            {
                StartCoroutine(ZombieAttack_Co());
            }
        } else
        {
            if (other.CompareTag("Scream") && !isScreamZombie)
            {
                targetPos = other.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            zombieAnim.SetBool("isPlayerFind", false);
        }
    }

    private IEnumerator ZombieAttack_Co()
    {
        zombieAnim.SetBool("isAttack", true);
        // Damage 넣어주기... todo
        yield return new WaitForSeconds(1.5f);
        zombieAnim.SetBool("isAttack", false);
    }

    private IEnumerator ZombieScream_Co()
    {
        // Player Sound Range에 없고, Scream Range에 있는 Zombie 불러오기
        zombieAnim.SetBool("isScream", true);
        screamRange.SetActive(true);
        yield return null;
        zombieAnim.SetBool("isScream", false);
        yield return new WaitForSeconds(10f);
        screamRange.SetActive(false);
    }
}
