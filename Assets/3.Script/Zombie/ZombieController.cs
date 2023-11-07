using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieAudio
{
    Hit = 0,
    Dead,
    Walk,
}

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
    public bool isNonTarget = true; // player가 target이 아닐 때

    private Animator zombieAnim;
    [SerializeField] private GameObject screamRange;
    private bool isScreamZombie = false;

    [Header("효과음")]
    private AudioSource zombieAudio;
    [SerializeField] private AudioClip[] audioClip;

    // ZombieData
    private SkinnedMeshRenderer skinned;

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        TryGetComponent(out zombieAudio);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        randomTarget = GameObject.FindGameObjectWithTag("RandomTarget").transform;
        skinned = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        StartCoroutine(RandomTargetPos_Co());
    }

    private void FixedUpdate()
    {
        ZombieTransToPlayer();
    }

    public void SetUp(ZombieData data)
    {
        // ZombieDataSetUp
        skinned.sharedMesh = data.skinnedMesh;
        isScreamZombie = data.isScreamZombie;
    }

    private void ZombieTransToPlayer()
    {
        // target에 따른 네비 적용
        nav.SetDestination(targetPos.position);
    }

    private IEnumerator RandomTargetPos_Co()
    {
        // random target position select
        if (isNonTarget)
        {
            if (RandomPoint(randomTarget.position, range, out point))
            {
                randomTarget.position = point;
            }
            targetPos = randomTarget;
            if (Vector3.Distance(randomTarget.position, transform.position) <= nav.stoppingDistance + 1.0f)
            {
                zombieAnim.SetBool("isIdle", true);
            }
            else
            {
                zombieAnim.SetBool("isIdle", false);
            }
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(RandomTargetPos_Co());
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // 플레이어 범위 밖일 때 랜덤 타겟 위치 설정, object 아닌 position으로만 따라갈 수 있도록 설정... todo
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
            isNonTarget = false;
            targetPos = player;
            zombieAnim.SetBool("isPlayerFind", true);
            if (isScreamZombie && Vector3.Distance(player.position, transform.position) > 1.5f)
            {
                StartCoroutine(ZombieScream_Co());
            }
        }
        if (other.CompareTag("Attack"))
        {
            // zombie Hit method... todo
            zombieAnim.SetTrigger("isDamage");
            zombieAudio.PlayOneShot(audioClip[(int)ZombieAudio.Hit]);
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
            isNonTarget = true;
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
