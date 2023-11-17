using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_isCar : MonoBehaviour
{
    public bool iscar = false;
    [SerializeField] private GameObject CarInfo;//차량상태창


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("CarDoor"))
        {
            iscar = true;
            Debug.Log("도어어어어");
        }
        if (other.gameObject.CompareTag("CarHood"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("후드으으으");
                CarInfo.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CarDoor"))
        {
            iscar = false;
        }

        if (other.gameObject.CompareTag("CarHood"))
        {
            CarInfo.SetActive(false);
        }
    }
}
