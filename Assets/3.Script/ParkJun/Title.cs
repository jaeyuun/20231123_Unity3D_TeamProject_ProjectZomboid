using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string newName = "GameNew";
    public string loadName = "GameLoad";

    public void ClickStart()
    {
        SceneManager.LoadScene(newName);
    }
    public void ClickLoad()
    {
        SceneManager.LoadScene(loadName);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
