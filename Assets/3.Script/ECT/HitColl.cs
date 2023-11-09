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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player_Attack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack"))
        {
            player.anim.SetTrigger("isHit");
            audioSource.PlayOneShot(Hit_Sound);
            StartCoroutine(Hit_co());
            Debug.Log("으악 아프다");
        }
    }

    public IEnumerator Hit_co()
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
        yield return new WaitForSeconds(0.3f);
    }
}
