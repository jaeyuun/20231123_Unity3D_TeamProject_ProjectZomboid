using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    //private GameObject[] objects;

    private AudioSource audio;
    [SerializeField]private AudioClip bottlesmash;

    private void Start()
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

       
    }
}
