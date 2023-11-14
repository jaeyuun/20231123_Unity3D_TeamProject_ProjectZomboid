using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject ppp;
    public GameObject Zombie_icon;
    public GameObject Carmer;
    public HitColl hitColl;
    private bool isDie=false;
    private bool Die = false;
    //private bool NonTarget = false;


    void Update()
    {
        
        if (hitColl.isDie && !Die)//Die로 예외처리 
        {
            Zombie.transform.position = Player.transform.position;//좀비를 고정시키는 옵션이라 예외처리가 필요함
            StartCoroutine(Die_Zombie_co());
            Die = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.CompareTag("Zombie")&& isDie)
        {
            Debug.Log("들어옴");
            other.gameObject.TryGetComponent(out ZombieController zombie);
            zombie.nonTarget = true;
        }
    }

    private IEnumerator Die_Zombie_co()
    {
        isDie = true;
        yield return new WaitForSeconds(5f);
        Player.SetActive(false);
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Zombie.SetActive(true);
        Carmer.GetComponent<Camera_Controller_Zomdie>().enabled = true;
        Zombie_icon.SetActive(true);


    }

}