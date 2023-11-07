using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shot : MonoBehaviour
{
    public Transform tip;//ÃÑ±¸
    public GameObject projectile;

    public void ShotEvent()
    {
        Instantiate(projectile, tip.transform.position, tip.transform.rotation);
    }

}
