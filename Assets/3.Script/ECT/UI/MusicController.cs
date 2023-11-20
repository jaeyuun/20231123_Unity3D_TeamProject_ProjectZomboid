using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#region Enum
public enum SliderList
{
    Master = 0,
    BGM,
    SFX,
}

public enum BGMSound
{
    Main = 0,
    Loading,
    Game,
}

public enum SFXSound
{
    Player_FootStep = 0,
    Player_Run,
    Player_Die,
    Player_BatSwing,
    Player_Hit,
    Zombie_Hit,
    Zombie_Die,
    Car_StartUp,
    Car_Dirve,
    Car_Brake,
    Car_InOut,
    Window_Bottele,
    Door_Open, // door open, broken, crash 찾기
    Door_Broken,
    Door_Crash,
    Gun_Shot,
    Rock_Hit,
    Rock_Broken,
}
#endregion
public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public AudioMixer audioMixer;

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips; // BGM 이외의 Audio Clip 전부 가져오기
    [SerializeField] private Slider[] slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(instance);
        }

        slider[(int)SliderList.Master].onValueChanged.AddListener(SetMasterVolume);
        slider[(int)SliderList.BGM].onValueChanged.AddListener(SetBGMVolume);
        slider[(int)SliderList.SFX].onValueChanged.AddListener(SetSFXVolume);

        PlayBGMSound("Main"); // GameStart
    }

    #region Sound Play
    public void PlayBGMSound(string type)
    {
        // 배경음 플레이
        if (bgmPlayer.isPlaying)
        {
            bgmPlayer.Stop();
        }
        int index = (int)(BGMSound)Enum.Parse(typeof(BGMSound), type); // string을 enum으로 변경 후 int로 변경
        bgmPlayer.clip = bgmClips[index];
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }
    public void PlaySFXSound(string type)
    {
        // 효과음 플레이
        int index = (int)(SFXSound)Enum.Parse(typeof(BGMSound), type);
        sfxPlayer.clip = sfxClips[index];
        sfxPlayer.PlayOneShot(sfxPlayer.clip);
    }
    #endregion
    #region Volume Setting
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    #endregion
}
