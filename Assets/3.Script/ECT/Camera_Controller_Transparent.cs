using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Transparent : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public float offset;  // 플레이어와 카메라의 거리
    public float zoomSpeed;  // 줌 속도
    public float minZoom;  // 최소 줌
    public float maxZoom;  // 최대 줌

    private float xOffset = 2f;
    private float zOffset = 3f;

    private void Start()
    {
        Camera_Early();
    }

    private void LateUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("EnvironmentObject"));

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();

            for(int j=0; j< obj.Length; j++)
            {
                obj[j]?.becomeTransParent();
            }
            
        }


        float scrollData = Input.GetAxis("Mouse ScrollWheel");  // 스크롤 데이터

        float nextOffset = offset - scrollData * (zoomSpeed*10);  // 다음 프레임에서의 offset 계산
        nextOffset = Mathf.Clamp(nextOffset, minZoom, maxZoom);  // 줌 제한

        // offset이 minZoom과 maxZoom 사이일 때만 xOffset, zOffset, offset 업데이트
        if (nextOffset > minZoom && nextOffset < maxZoom)
        {
            if (scrollData > 0) // 스크롤을 올렸을 때
            {
                xOffset -= zoomSpeed; // X와 Z축에 대한 오프셋을 줄임.
                zOffset -= zoomSpeed;
                offset = nextOffset;
            }
            else if (scrollData < 0) // 스크롤을 내렸을 때
            {
                xOffset += zoomSpeed;
                zOffset += zoomSpeed; // X와 Z축에 대한 오프셋을 늘림.
                offset = nextOffset;
            }
        }

        transform.position = player.position + new Vector3(-xOffset, offset, -zOffset);  // 카메라 위치 업데이트
    }

    private void Camera_Early()
    {
        transform.position = player.position + new Vector3(-xOffset, offset, -zOffset);  // 초기 카메라 위치 설정

    }
}