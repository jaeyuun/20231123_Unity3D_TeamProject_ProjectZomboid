using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColl : MonoBehaviour
{
    [Header("B_pos, hit오브젝트")]
    public Transform Hit_pos;//피튀는곳
    public GameObject hit;//피오브젝트선언

    [Header("피격(사운드클립)")]
    private AudioSource audioSource;
    public AudioClip Hit_Sound;

    [Header("플레이어 넣어주세요")]
    public Player_Attack player;

    public HP hp;
    private float Player_HP;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player_Attack>();
        hp = GetComponentInParent<HP>();
        Player_HP=hp.Start_HP(Player_HP);
        Debug.Log(Player_HP);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack"))
        {
            player.anim.SetTrigger("isHit");
            audioSource.PlayOneShot(Hit_Sound);
            StartCoroutine(Hit_co());
            Player_HP= hp.Damage(30f, Player_HP);
            Debug.Log(Player_HP);
        }
        if(Player_HP<=0)
        {
            player.anim.SetTrigger("isDie");
            Debug.Log("끄아아아아아아아앜!");
            player.GetComponent<Player_Move>().enabled = false;
            player.GetComponent<Player_Attack>().enabled = false;
            
        }
    }

    public IEnumerator Hit_co()
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
        yield return new WaitForSeconds(0.3f);
    }
}
