using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public StatusController theStat;

    private void Start()
    {
       // theStat = GetComponent<StatusController>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theStat.DecreaseHP(25);
        }
    }
}
