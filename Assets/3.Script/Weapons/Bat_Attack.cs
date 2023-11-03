using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Attack : MonoBehaviour
{
    public GameObject sound;
    public Player_Attack player_Attack;
    public AudioClip BatHit;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player_Attack = GameObject.Find("Player_MHK").GetComponent<Player_Attack>();//Player 게임오브젝트 찾기
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Zombie") && player_Attack.isAttack)
        {
            sound.SetActive(true);
            audioSource.PlayOneShot(BatHit);
        }
        player_Attack.isAttack = false;
        sound.SetActive(false);
    }

}
