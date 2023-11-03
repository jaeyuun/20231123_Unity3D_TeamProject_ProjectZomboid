using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Attack : MonoBehaviour
{
   public GameObject sound;
   public Player_Attack player_Attack;
    void Start()
    {
        player_Attack = GameObject.Find("Player").GetComponent<Player_Attack>();//플레이어를 찾아서 공격
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Zombie")&& player_Attack.isAttack)
        {
            Debug.Log("때렸다.");
            sound.SetActive(true);            
        }
        player_Attack.isAttack = false;
        sound.SetActive(false);
    }
}
