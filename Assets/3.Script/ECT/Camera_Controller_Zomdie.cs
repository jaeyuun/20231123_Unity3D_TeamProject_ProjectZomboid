using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Zomdie : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform

    public float zoomSpeed;  // 줌 속도
    public float minZoom;  // 최소 줌
    public float maxZoom;  // 최대 줌

    [Header("기본오프셋")]
    public float Yoffset;  // 플레이어와 카메라의 거리
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;

    private void Start()
    {
        //Camera_Early();
    }

    private void LateUpdate()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");  // 스크롤 데이터

        float nextOffset = Yoffset - scrollData * (zoomSpeed*10);  // 다음 프레임에서의 offset 계산
        nextOffset = Mathf.Clamp(nextOffset, minZoom, maxZoom);  // 줌 제한

        // offset이 minZoom과 maxZoom 사이일 때만 xOffset, zOffset, offset 업데이트
        if (nextOffset > minZoom && nextOffset < maxZoom)
        {
            if (scrollData > 0) // 스크롤을 올렸을 때
            {
                xOffset -= zoomSpeed; // X와 Z축에 대한 오프셋을 줄임.
                zOffset -= zoomSpeed;
                Yoffset = nextOffset;
            }
            else if (scrollData < 0) // 스크롤을 내렸을 때
            {
                xOffset += zoomSpeed;
                zOffset += zoomSpeed; // X와 Z축에 대한 오프셋을 늘림.
                Yoffset = nextOffset;
            }
        }


        transform.position = player.position + new Vector3(-xOffset, Yoffset, -zOffset);  // 카메라 위치 업데이트

    }

    private void Camera_Early()
    {

        transform.position = player.position + new Vector3(-8, 16, -8);  // 초기 카메라 위치 설정

    }

}