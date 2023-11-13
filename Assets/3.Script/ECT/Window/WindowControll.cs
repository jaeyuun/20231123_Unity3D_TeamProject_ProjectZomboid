using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControll : MonoBehaviour
{
    //private GameObject[] objects;
    private float distance = 10f;
    GameObject window;
    private float hitCount;


    //private AudioSource audio;
    [SerializeField]private AudioClip bottlesmash;

    /* private void Start()
     {
         audio = GetComponent<AudioSource>();
     }
     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.E))
         {
             GameObject[] windows = GameObject.FindGameObjectsWithTag("Window");




             foreach (GameObject window in windows)
             {               
                 audio.PlayOneShot(bottlesmash);
                 window.SetActive(false);
             }
         }


     }*/// Basic

   
    private void Update()
    {
        WindowHit();
    }

    private void WindowHit()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 1.2f, 0), transform.forward);
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 1.2f, 0), transform.forward * 10f, Color.red);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                
                //Debug.Log(hit.collider.gameObject.CompareTag("Window"));
                if (hit.collider.gameObject.CompareTag("Window"))
                {
                    Debug.Log("hit");
                    window = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    
                    window.SetActive(false);
                }

                if (hit.collider.gameObject.CompareTag("WindowHit"))
                {

                    if (hitCount >= 3)
                    {

                        WIndow_bool window = hit.collider.GetComponentInChildren<WIndow_bool>();
                        if (window != null)
                        {
                            hitCount++;
                            Debug.Log(hitCount);
                            window.isOpen = !window.isOpen;
                        }
                    }

                }


            }

        }

    }


}
