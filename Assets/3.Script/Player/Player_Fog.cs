using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Fog : MonoBehaviour
{
    [Range(0f, 360f)] [SerializeField] public float viewAngle = 130f; // 감지하는 범위 각도
    [SerializeField] public float ViewRadius = 2f; // 감지 범위
    [SerializeField] private LayerMask TargetMask; // 타겟 인식 레이어, Player
    [SerializeField] private LayerMask ObstacleMask;
    private List<Collider> hitTargetList = new List<Collider>(); // 감지한 타겟 리스트
    
    public Vector3 myPos;
    public float lookingAngle;
    public Vector3 lookDir;

    private void Awake()
    {
       
    }



    private void OnDrawGizmos()
    {
        myPos = transform.position + Vector3.up * 1.5f; // 캐릭터 포지션
        Gizmos.DrawWireSphere(myPos, ViewRadius);

        lookingAngle = transform.eulerAngles.y; //캐릭터가 바라보는 방향의 각도
        lookDir = AngleToDir(lookingAngle);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        // Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider[] targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (targets.Length == 0) return;
        foreach (Collider playerColli in targets)
        {
            Vector3 targetPos = playerColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;

            if (playerColli.transform.childCount > 0) // 자식 오브젝트 존재 확인
            {
                if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
                {
                    hitTargetList.Add(playerColli);
                    playerColli.transform.GetChild(0).gameObject.SetActive(true);
                    Debug.DrawLine(myPos, targetPos, Color.red);
                }
                else
                {
                    playerColli.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        /* foreach (Collider playerColli in targets)
         { // target list

             Vector3 targetPos = playerColli.transform.position;
             Vector3 targetDir = (targetPos - myPos).normalized;
             float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
             if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
             {
                 hitTargetList.Add(playerColli);
                 playerColli.transform.GetChild(0).gameObject.SetActive(true);
                 Debug.DrawLine(myPos, targetPos, Color.red);
             }
             else
             {
                 playerColli.transform.GetChild(0).gameObject.SetActive(false);
             }


         }*/





    }

    public Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
}


/* //
 private float viewAngle = 130f; // 감지하는 범위 각도
 [SerializeField] private float ViewRadius = 2f; // 감지 범위
 [SerializeField] private LayerMask TargetMask; // 타겟 인식 레이어, Player

 private void Update()
 {
     GameObject[] secondtag = GameObject.FindGameObjectsWithTag(targetTag);

     // 카메라와 플레이어의 중간 지점
     Vector3 midPoint = (AA.transform.position + transform.position) / 2f;

     // 특정 태그를 가진 오브젝트
     GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

     // 찾은 오브젝트들을 삭제
     foreach (GameObject obj in objectsWithTag)
     {
         // 오브젝트와 중간 지점 간의 거리를 계산
         float distance = Vector3.Distance(midPoint, obj.transform.position);

         // 삭제 반경 내에 있는 오브젝트만 삭제
         if (distance <= deletionRadius)
         {
             obj.transform.GetChild(0).gameObject.SetActive(false);
         }
         else
         {
             obj.transform.GetChild(0).gameObject.SetActive(true);
         }
     }
 }

}
*/