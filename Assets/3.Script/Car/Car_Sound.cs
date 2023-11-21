using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Sound : MonoBehaviour
{
    /*[Header("矫悼家府")]
    public AudioClip start_up;// 矫悼家府
    [Header("款傈家府")]
    public AudioClip drive;// 款傈家府
    [Header("宏饭捞农家府")]
    public AudioClip brake;// 款傈家府

    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
*/
    private bool isDriveSoundPlaying = false;

    private IEnumerator Drive_Sound()
    {
        if (isDriveSoundPlaying)
        {
            yield break;
        }

        isDriveSoundPlaying = true;
        // audioSource.PlayOneShot(drive);
        MusicController.instance.PlaySFXSound("Car_Dirve");
        yield return new WaitForSeconds(0.9f);

        isDriveSoundPlaying = false;
    }

    public void Start_up()//矫悼家府
    {
        // audioSource.PlayOneShot(start_up);
        MusicController.instance.PlaySFXSound("Car_StartUp");
    }

    public void Drive()//款傈家府
    {
        StartCoroutine(Drive_Sound());
    }


    private bool isBrakeSoundPlaying = false;

    private IEnumerator Brake_Sound()
    {
        if (isBrakeSoundPlaying)
        {
            yield break;
        }

        isBrakeSoundPlaying = true;
        // audioSource.PlayOneShot(brake);
        MusicController.instance.PlaySFXSound("Car_Brake");
        yield return new WaitForSeconds(1.5f);

        isBrakeSoundPlaying = false;
    }
    public void Brake()//宏饭捞农
    {
        StartCoroutine(Brake_Sound());
    }

}
