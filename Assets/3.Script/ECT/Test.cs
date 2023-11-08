using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sound"))
        {
            Debug.Log("Enter");
        }
    }

/*    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Sound"))
        {
            Debug.Log("Stay");
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("¶³¾îÁü");
        if (other.gameObject.CompareTag("Sound"))
        {
            Debug.Log("¶³¾îÁü");
        }
    }
}
