using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_isCar : MonoBehaviour
{
    public bool iscar = false;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Car"))
        {
            iscar = true;
            Debug.Log("iscar");
        }
   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            iscar = false;
            Debug.Log("iscar");
        }
    }
}
