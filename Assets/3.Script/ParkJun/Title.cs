using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string NewName = "GameNew";
    public string loadName = "GameLoad";

    public void ClickStart()
    {
        MusicController.instance.ChangeSceneMusic("Load");
        SceneManager.LoadScene(NewName);
    }
    public void ClickLoad()
    {
        MusicController.instance.ChangeSceneMusic("Load");
        SceneManager.LoadScene(loadName);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
   
}
