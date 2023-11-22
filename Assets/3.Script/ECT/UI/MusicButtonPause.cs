using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButtonPause : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.isPause)
            {
                MusicController.instance.AwakeSetting();
                MusicController.instance.SettingButton();
                MusicController.instance.OnEnableMusic();
            }
        }   
    }
}
