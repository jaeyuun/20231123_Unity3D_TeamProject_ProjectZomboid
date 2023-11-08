using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFieldOfView : MonoBehaviour
{
    private ZombieController zombieController;
    // [Range(0f, 360f)] [SerializeField]
    private float viewAngle = 130f; // 감지하는 범위 각도
    [SerializeField] private float ViewRadius = 2f; // 감지 범위
    [SerializeField] private LayerMask TargetMask; // 타겟 인식 레이어, Player
    [SerializeField] private LayerMask ObstacleMask;
    private List<Collider> hitTargetList = new List<Collider>(); // 감지한 타겟 리스트

    private void Awake()
    {
        TryGetComponent(out zombieController);
    }

    private void PlayerFind(Vector3 targetPos)
    {
        zombieController.targetPos = targetPos;
    }

    private void OnDrawGizmos()
    {
        Vector3 myPos = transform.position + Vector3.up * 1.5f; // 캐릭터 포지션
        Gizmos.DrawWireSphere(myPos, ViewRadius);

        float lookingAngle = transform.eulerAngles.y; //캐릭터가 바라보는 방향의 각도
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider[] targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (targets.Length == 0) return;

        foreach (Collider playerColli in targets)
        { // target list
            Vector3 targetPos = playerColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                hitTargetList.Add(playerColli);
                PlayerFind(targetPos); // target 위치 변경
                Debug.DrawLine(myPos, targetPos, Color.red);
            }
        }
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
}
