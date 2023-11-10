using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    public GameObject LightObject;
    private float InterectiveDistance = 10f;
    Door_bool door_Bool;


    private void Update()
    {
        DoorHit();
    }

    private void DoorHit()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 1.2f, 0), transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position + new Vector3(0, 1.2f, 0), transform.forward * 5f, Color.white);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            if (Physics.Raycast(ray, out hit, InterectiveDistance))
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider.gameObject.CompareTag("Door"))
                {
                    Debug.Log("hit Door");
                    //hit.collider.GetComponentInChildren<Door_bool>().isOpen = true;
                    Door_bool door = hit.collider.GetComponentInChildren<Door_bool>();

                    if (door != null)
                    {
                        door.isOpen = !door.isOpen;
                    }
                }
            }
        }
    }
}


