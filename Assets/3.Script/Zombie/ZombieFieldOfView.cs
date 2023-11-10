using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFieldOfView : MonoBehaviour
{
    private ZombieController zombieController;
    // [Range(0f, 360f)] [SerializeField]
    private float viewAngle = 130f; // 감지하는 범위 각도
    [SerializeField] private float viewRadius = 15f; // 감지 범위
    [SerializeField] private LayerMask targetMask; // 타겟 인식 레이어, Player
    [SerializeField] private LayerMask objectMask; // 좀비가 인식할 Object 레이어 
    [SerializeField] private LayerMask obstacleMask;
    private List<Collider> hitTargetList = new List<Collider>(); // 감지한 타겟 리스트

    public Vector3 zombiePos;
    public float lookingAngle;
    public Vector3 lookDir;

    private Vector3 targetPos;

    private void Awake()
    {
        TryGetComponent(out zombieController);
    }

    private void Update()
    {
        JombieClimbing();
    }

    private void ZombieTargeting(Vector3 targetPos)
    {
        zombieController.targetPos = targetPos;
    }

    private void OnDrawGizmos()
    {
        zombiePos = transform.position + Vector3.up * 1.5f; // 좀비 포지션
        Gizmos.DrawWireSphere(zombiePos, viewRadius);

        lookingAngle = transform.eulerAngles.y; // 좀비가 바라보는 방향의 각도
        lookDir = AngleToDir(lookingAngle);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);

        Debug.DrawRay(zombiePos, rightDir * viewRadius, Color.blue);
        Debug.DrawRay(zombiePos, leftDir * viewRadius, Color.blue);
        Debug.DrawRay(zombiePos, lookDir * viewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider[] targets = Physics.OverlapSphere(zombiePos, viewRadius, targetMask);

        if (targets.Length.Equals(0)) return;

        foreach (Collider playerColli in targets)
        { // target list
            Vector3 playerPos = playerColli.transform.position;
            Vector3 targetDir = (playerPos - zombiePos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(zombiePos, targetDir, viewRadius, obstacleMask))
            {
                hitTargetList.Add(playerColli);
                ZombieTargeting(playerPos); // target 위치 playerPos로 변경
                Debug.DrawLine(zombiePos, playerPos, Color.red);
            }
        }
    }

    public Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void JombieClimbing()
    {
        // IState로 옮길수도 있습니다.
        RaycastHit hit;

        if (Physics.Raycast(zombiePos, lookDir, out hit))
        {
            if (hit.collider.CompareTag("Window"))
            {
                zombieController.targetPos = hit.collider.gameObject.transform.position; // 윈도우 위치때문에 움직임
                zombieController.targetPos = targetPos;
                if (Vector3.Distance(targetPos, transform.position) <= 1.5f)
                {
                    // jump anim
                    Debug.Log("Window");
                }
            }
            else if (hit.collider.CompareTag("Fence"))
            {
                if (Vector3.Distance(targetPos, transform.position) <= 1.5f)
                {
                    // jump anim
                    Debug.Log("Fence");
                }
            }
            else if (hit.collider.CompareTag("Door"))
            {
                Door_bool door = hit.collider.GetComponentInChildren<Door_bool>();
                if (!door.isOpen)
                {
                    // attack coroutine
                    if (Vector3.Distance(targetPos, transform.position) <= 1.5f)
                    {
                        // jump anim
                        Debug.Log("Door");
                    }
                }
            }
        }
    }
}
