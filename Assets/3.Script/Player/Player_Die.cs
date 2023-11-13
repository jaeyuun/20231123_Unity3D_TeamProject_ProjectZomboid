using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Zombie;
    public GameObject Zombie_icon;
    public GameObject Carmer;
    public HitColl hitColl;
    private bool a=false;

    void Start()
    {
       
    }

    void Update()
    {
        
        if (hitColl.isDie)
        {
            Zombie.transform.position = Player.transform.position;
            StartCoroutine(Die_Zombie_co());
        }
    }

    private IEnumerator Die_Zombie_co()
    {
        yield return new WaitForSeconds(5f);
        Player.SetActive(false);
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Zombie.SetActive(true);
        Carmer.GetComponent<Camera_Controller_Zomdie>().enabled = true;
        Zombie_icon.SetActive(true);
    }
}