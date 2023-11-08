using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieAudio
{
    Hit = 0,
    Dead,
    Idle,
    Walk,
}

public class ZombieController : MonoBehaviour, IState
{
    // Zombie NavMesh
    private NavMeshAgent nav;
    public Vector3 targetPos;
    private Vector3 randomPos; // 플레이어 감지하지 않았을 때 위치
    private Vector3 playerPos; // 플레이어의 위치
    private bool isTarget = false;

    // RandomTarget NavMesh
    [SerializeField] private float range = 10f;

    private Animator zombieAnim;
    [SerializeField] private GameObject screamRange;
    private bool isScreamZombie = false;

    [Header("효과음")]
    private AudioSource zombieAudio;
    [SerializeField] private AudioClip[] audioClip;

    // ZombieData
    private SkinnedMeshRenderer skinned;

    // ZombieAttack Collider
    [SerializeField] private Collider[] zombieAttackCol;

    private void Awake()
    {
        TryGetComponent(out nav);
        TryGetComponent(out zombieAnim);
        TryGetComponent(out zombieAudio);
        /*if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }*/
        skinned = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            zombieAttackCol[i].enabled = false;
        }
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
        nav.SetDestination(targetPos);
    }

    private IEnumerator RandomTargetPos_Co()
    {
        randomPos = GetRandomPosOnNav();
        targetPos = randomPos;
        yield return new WaitForSeconds(5f);
        StartCoroutine(RandomTargetPos_Co());
    }

    private Vector3 GetRandomPosOnNav()
    {
        Vector3 randomDir = Random.insideUnitSphere * range;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 20f, NavMesh.AllAreas))
        {
            return hit.position; // navmesh 위의 랜덤 위치 반환
        }
        else
        {
            return transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            playerPos = other.transform.position;
            targetPos = playerPos;
            zombieAnim.SetBool("isPlayerFind", true);
            if (isScreamZombie)
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
            playerPos = other.transform.position; // Update the player position
            isTarget = true;
            targetPos = playerPos;
            Debug.Log("소리들음");
            if (Vector3.Distance(targetPos, transform.position) <= 3f)
            {
                Debug.Log("접근");
                StartCoroutine(ZombieAttack_Co());
            }
            Debug.Log("소리듣기만함");
        }
        else
        {
            if (other.CompareTag("Scream") && !isScreamZombie)
            {
                targetPos = other.gameObject.transform.position; // screamZombie position
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sound"))
        {
            isTarget = false;
            zombieAnim.SetBool("isPlayerFind", false);
        }
    }

    private IEnumerator ZombieAttack_Co()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        zombieAnim.SetBool("isAttack", true);
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            // Attack할 때만 Collider enable True
            zombieAttackCol[i].enabled = true;
        }
        // Damage 넣어주기... todo
        yield return new WaitForSeconds(1.5f);
        nav.isStopped = false;
        zombieAnim.SetBool("isAttack", false);
        for (int i = 0; i < zombieAttackCol.Length; i++)
        {
            zombieAttackCol[i].enabled = false;
        }
    }

    private IEnumerator ZombieScream_Co()
    {
        // Player Sound Range에 없고, Scream Range에 있는 Zombie 불러오기
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        zombieAnim.SetBool("isScream", true);
        screamRange.SetActive(true);
        yield return null;
        nav.isStopped = false;
        zombieAnim.SetBool("isScream", false);
        yield return new WaitForSeconds(10f);
        screamRange.SetActive(false);
    }

    public void Idle()
    {
        if (!isTarget && Vector3.Distance(targetPos, transform.position) <= 0.5f)
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            zombieAnim.SetBool("isIdle", true);
        }
        else
        {
            nav.isStopped = false;
            zombieAnim.SetBool("isIdle", false);
        }
    }
}
