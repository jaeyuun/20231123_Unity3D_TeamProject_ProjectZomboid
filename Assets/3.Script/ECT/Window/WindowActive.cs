using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowActive : MonoBehaviour
{
    public float Radius = 5f;
    private GameObject window;
    private int hitCount = 0;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

        if(Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Window"))
                {
                    //Debug.Log("hit");

                    window = collider.gameObject.transform.GetChild(0).gameObject;
                    //window = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    window.SetActive(false);
                }

                if (collider.gameObject.CompareTag("WindowHit"))
                {

                    if (hitCount >= 3)
                    {

                        WIndow_bool window = collider.GetComponentInChildren<WIndow_bool>();
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
