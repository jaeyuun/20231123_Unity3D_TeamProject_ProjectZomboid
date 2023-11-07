using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shot : MonoBehaviour
{
    public Transform tip;//ÃÑ±¸
    public GameObject projectile;
    public AudioClip gunShot;
    private AudioSource audioSource;
    public GameObject Sound_Gun;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void ShotEvent()
    {
        Instantiate(projectile, tip.transform.position, tip.transform.rotation);
        audioSource.PlayOneShot(gunShot);
        Sound_Gun.SetActive(true);
    }

}
