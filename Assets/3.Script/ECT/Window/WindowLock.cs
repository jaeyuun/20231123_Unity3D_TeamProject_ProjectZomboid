using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLock : MonoBehaviour
{
    private float distance = 10f;
    WIndow_bool wIndow_Bool;
    private int hitCount;
    public bool isOpen;
   // private AudioSource audio;
   // [SerializeField]private AudioClip bottlesmash;

   
    private void Update()
    {
       
        if (hitCount >= 3)
        {
            rayHit();
        }

    }

    private void rayHit()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 2f, 0), transform.forward, Color.red, distance);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.gameObject.CompareTag("WindowHit"))
                {
                    window(hit);
                }
            }
        }
    }
    private void window(RaycastHit raycastHit)
    {

        WIndow_bool window = raycastHit.collider.GetComponentInChildren<WIndow_bool>();

        hitCount++;
        if (window != null)
        {
            window.isOpen = !window.isOpen;
        }
    }




}
