using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindowActive : MonoBehaviour
{
    // Player_Move에 달려있음
    public float radius = 1f;
    private Collider windowCollider;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.5f, 0), radius);
    }

    private void Update()
    {
        WindowInteraction();
    }

    private void WindowInteraction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0, 1.5f, 0), radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Window"))
            {
                windowCollider = collider;
                // window tag는 깨진 창문 모델링에 달아두기
                if (Input.GetKeyDown(KeyCode.E))
                {
                    WindowOpen();
                };
            }
        }
    }

    public void WindowOpen()
    {
        // player가 창문 여는 키 눌렀을 때
        WIndow_bool window = windowCollider.GetComponent<WIndow_bool>();
        if (!window.isBroken)
        {
            window.WindowAnimation();
        }
    }

}