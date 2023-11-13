using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Zombie;
    public GameObject Carmer;
    public HitColl hitColl;

    void Start()
    {
        Player = GetComponentInChildren<GameObject>();
        Zombie = GetComponentInChildren<GameObject>();
        hitColl = GetComponent<HitColl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hitColl.isDie)
        {
            Zombie.transform.position = Player.transform.position;
            Player.SetActive(false);
            Zombie.SetActive(true);
            Carmer.GetComponent<Camera_Controller>().enabled = false;
            Carmer.GetComponent<Camera_Controller_Zomdie>().enabled = true;
        }
    }
}
