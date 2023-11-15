using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Hp : MonoBehaviour
{

    private int door_hp = 20;
    private AudioSource audioSource;
    [Header("滴靛府绰 家府甫 持绢林技夸")]
    public AudioClip Door_crash;
    [Header("何辑瘤绰 家府甫 持绢林技夸")]
    public AudioClip Door_broken;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ZombieAttack"))
        {
            StartCoroutine(door_hit_co());
            if(door_hp<=0)
            {
                audioSource.PlayOneShot(Door_broken);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator door_hit_co()
    {
        door_hp -= 1;
        audioSource.PlayOneShot(Door_crash);
        yield return new WaitForSeconds(2f);

    }
}
