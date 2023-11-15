using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainControll : MonoBehaviour
{
    //커튼 활성/ 비활성화
    //활성 - 빛 차단 - 손전등 여부

    private GameObject curtain;
    public GameObject curtainDown;
    [SerializeField]private Light getlight;
    private bool turnOn = false;
    public float radius = 0;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 1.5f, radius);
    }

    private void Update()
    {
        curtainSet();
    }

    private void curtainSet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up * 1.5f, radius);


        foreach (Collider collider in colliders)
        {
                    Debug.Log(collider.tag);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collider.CompareTag("Curtain"))
                {
                    curtain = collider.transform.GetChild(0).gameObject;
                    curtainDown = collider.transform.GetChild(1).gameObject;
                    if (curtain.activeSelf)
                    {
                        curtain.SetActive(false);
                        curtainDown.SetActive(true);
                    }
                    else
                    {
                        curtain.SetActive(true);
                        curtainDown.SetActive(false);
                    }
                }

                /*if (collider.CompareTag("CurtainDown"))
                {
                    curtainDown = collider.transform.GetChild(0).gameObject;
                    Debug.Log("생겨라");
                    curtainDown.SetActive(true);
                    curtain = collider.transform.gameObject;
                    Debug.Log(curtain);
                    curtain.SetActive(false);
                    turnOnOff();
                }*/
            }
        }
    }

    private void turnOnOff()
    {
        if(turnOn)
        {
            getlight.intensity = 1f;
        }
        else
        {
            getlight.intensity = 0f;
        }
    }
}
