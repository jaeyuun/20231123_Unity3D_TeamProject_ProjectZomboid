using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private SaveAndLoad theSaveAndLoad;
    public string sceneName = "TestIntro";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.isPause)
            {
                CallMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }
    private void CallMenu()
    {
        GameManager.isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        GameManager.isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;

    }
    public void ClickSave()
    {
        Debug.Log("세이브 ");
        theSaveAndLoad.SaveData();
    }
    public void ClicKLoad()
    {
        Debug.Log("로드 ");
        theSaveAndLoad.LoadData();
    }
    public void ClickExit()
    {
        Debug.Log("종료 ");
        Application.Quit();
    }
    public void ClickIntroSave()
    {
        Debug.Log("세이브 ");
        //theSaveAndLoad.SaveData();
        SceneManager.LoadScene(sceneName);

    }
}

