using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Canvas canvas;

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    [SerializeField] private GameObject musicUI;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips; // BGM 이외의 Audio Clip 전부 가져오기
    [SerializeField] private GameObject[] sliderObject;
    [SerializeField] private Slider[] slider;

    private Button settingButton;
    private GameObject musicSettingPanel = null;

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

        TryGetComponent(out bgmPlayer);
        TryGetComponent(out sfxPlayer);

        canvas = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
        settingButton = GameObject.FindGameObjectWithTag("SettingButton").transform.GetComponent<Button>();
        settingButton.onClick.AddListener(SetActiveTrue);
        settingButton.onClick.AddListener(SetActiveFalse);
    }

    private void OnEnable()
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
        }
        canvas = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
        if (settingButton == null)
        {
            settingButton = GameObject.FindGameObjectWithTag("SettingButton").transform.GetComponent<Button>();
            Debug.Log("AddListener");
            settingButton.onClick.AddListener(SetActiveTrue);
            settingButton.onClick.AddListener(SetActiveFalse);
        }
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveAllListeners(); //이벤트 모두 제거
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("TestIntro_UIFix")) // IntroScene name
        {
            PlayBGMSound("Main"); // GameStart
        }
    }

    private void SetActiveTrue()
    {
        Debug.Log("SetActive");
        if (!musicSettingPanel.activeSelf)
        {
            musicSettingPanel.SetActive(true);
        }
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
                slider[(int)SliderList.Master].onValueChanged.AddListener(SetMasterVolume);
                slider[(int)SliderList.BGM].onValueChanged.AddListener(SetBGMVolume);
                slider[(int)SliderList.SFX].onValueChanged.AddListener(SetSFXVolume);
            }
        }
    }

    private void SetActiveFalse()
    {
        // Menu activeSelf false
        if (musicSettingPanel.activeSelf)
        {
            musicSettingPanel.SetActive(false);
        }
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
        // audioMixer.SetFloat("Master", volume);
        SetBGMVolume(volume);
        SetSFXVolume(volume);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
        AudioListenerVolume("BGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        AudioListenerVolume("SFX", volume);
    }

    private void AudioListenerVolume(string type, float volume)
    {
        // AudioSource 할당
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
