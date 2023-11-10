using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControll : MonoBehaviour
{
    //private GameObject[] objects;
    private float distance = 10f;
    GameObject window;


    private AudioSource audio;
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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        //Debug.DrawRay(transform.position + new Vector3(0, 2f, 0), transform.forward, Color.white, Distance);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                //Debug.Log(hit.collider.gameObject.CompareTag("Window"));
                if (hit.collider.gameObject.CompareTag("Window"))
                {
                    window = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    // Debug.Log("hit");
                    window.SetActive(false);
                }
            }
        }

    }

}
