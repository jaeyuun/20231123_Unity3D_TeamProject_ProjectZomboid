using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{

    public string TargetTag = "Window";
    private GameObject[] objects;
    private int currentObject = 0;
    private bool isInterection = false;
    private Player_Attack plyer_Attack;
         
    private void Start()
    {
        objects = GameObject.FindGameObjectsWithTag(TargetTag);
        isInterection = false;
        plyer_Attack = new Player_Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Window")&& !isInterection)
        {
            if(Input.GetMouseButton(1))
            {
                isInterection = true;
                StartInterective();               
            }
            Debug.Log("E");
        }
    }
    private void StartInterective()
    {
        //오브젝트 활성화
        if(currentObject< objects.Length)
        {
            GameObject targetobject = objects[currentObject];
            targetobject.SetActive(true);
            currentObject++;
        }
        //모든 창이 깨짐 
        else
        {
            isInterection = false;
        }
    }

}
