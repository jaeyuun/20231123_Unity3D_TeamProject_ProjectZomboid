using UnityEngine;

public class SecondFloor_Except : MonoBehaviour
{
    public string targetTag = "Except";
    public string floorDelet = "2floor";
    public float deletionRadius = 20f; 
    public Camera mainCamera; 


    private void Update()
    {
        // 카메라와 플레이어의 중간 지점
        Vector3 midPoint = (mainCamera.transform.position + transform.position) / 2f;

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
                Destroy(obj);
            }
        }
    }
}
