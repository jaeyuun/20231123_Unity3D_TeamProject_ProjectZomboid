using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_MHK : MonoBehaviour
{
    public float forceMagnitude = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                otherRigidbody.AddForce(-forceDirection * forceMagnitude, ForceMode.Impulse);
                other.gameObject.SetActive(false);
            }
        }
    }
}
