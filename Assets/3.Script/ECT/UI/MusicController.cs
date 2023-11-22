using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using DigitalRuby.RainMaker;

#region Enum
public enum SliderList
{
    SFX = 0,
    BGM,
}

public enum BGMSound
{
    Intro = 0,
    GameLoad,
    GameNew,
    MainGame_Fake,
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
    Door_Open,
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
    private Canvas canvas;

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    [SerializeField] private GameObject musicUI;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private GameObject[] sliderObject;
    [SerializeField] private Slider[] slider;

    private Button settingButton;
    private GameObject musicSettingPanel;

    public float bgmVolume = 0f;
    public float sfxVolume = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            PlayerPrefs.SetFloat("BGMVolume", 0f);
            PlayerPrefs.SetFloat("SFXVolume", 0f);
            instance = this;
            DontDestroyOnLoad(instance);
        } 
        else
        {
            instance.ChangeSceneMusic();
            Destroy(gameObject);
        }

        // audioSource
        bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        sfxPlayer = transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ChangeSceneMusic();
    }

    public void ChangeSceneMusic()
    {
        // Scene이 바뀔때 출력되는 메소드
        AwakeSetting();
        SettingButton();
        PlayBGMSound();
    }

    public void AwakeSetting()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
            musicSettingPanel = null;
        }
    }

    public void SettingButton()
    {
        settingButton = null;
        if (settingButton == null)
        {
            /*if (settingButton.onClick != null)
            {
                settingButton.onClick.RemoveAllListeners(); // Event Remove All
            }*/
            settingButton = GameObject.FindGameObjectWithTag("SettingMenu").transform.GetChild(2).gameObject.transform.GetComponent<Button>();
            settingButton.onClick.AddListener(SetActiveTrue);
        }
    }

    public void SliderMusicSetting()
    {
        // Sound PlayerPrefs Check
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
            slider[(int)SliderList.BGM].value = PlayerPrefs.GetFloat("BGMVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            slider[(int)SliderList.SFX].value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    private void SetActiveTrue()
    {
        // Setting Button Click, Menu activeSelf true
        if (musicSettingPanel == null)
        {
            musicSettingPanel = Instantiate(musicUI, canvas.transform);
            sliderObject = GameObject.FindGameObjectsWithTag("MusicSlider");
            if (musicSettingPanel.activeSelf)
            {
                for (int i = 0; i < sliderObject.Length; i++)
                {
                    slider[i] = sliderObject[i].transform.GetComponent<Slider>();
                }
                slider[(int)SliderList.BGM].onValueChanged.AddListener(SetBGMVolume);
                slider[(int)SliderList.SFX].onValueChanged.AddListener(SetSFXVolume);
            }
        }
        if (!musicSettingPanel.activeSelf)
        {
            musicSettingPanel.SetActive(true);
        }
        SliderMusicSetting();
    }

    #region Sound Play
    public void PlayBGMSound()
    {
        string type = SceneManager.GetActiveScene().name;
        Debug.Log(type);
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
        int index = (int)(SFXSound)Enum.Parse(typeof(SFXSound), type);
        sfxPlayer.clip = sfxClips[index];
        sfxPlayer.PlayOneShot(sfxPlayer.clip);
    }
    #endregion
    #region Volume Setting
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
        bgmVolume = volume;
        AudioListenerVolume("BGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        sfxVolume = volume;
        AudioListenerVolume("SFX", volume);
    }

    private void AudioListenerVolume(string type, float volume)
    {
        // AudioSource 할당, volume에 따른 음소거 설정
        AudioSource typeAudio = null;
        if (type.Equals("BGM"))
        {
            typeAudio = bgmPlayer;
        }
        else if (type.Equals("SFX"))
        {
            typeAudio = sfxPlayer;
        }
        // Audio mute
        if (volume.Equals(-80f))
        {
            typeAudio.mute = true;
        }
        else
        {
            typeAudio.mute = false;
        }
    }
    #endregion
}
