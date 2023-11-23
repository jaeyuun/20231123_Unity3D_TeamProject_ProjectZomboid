using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private SaveAndLoad theSaveAndLoad;
    private string sceneName = "Intro";
    private string newscene = "GameNew";


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    public void ClickReturn()
    {
        CloseMenu();
       
    }
    public void ClickExit()
    {
        
        Application.Quit();
    }
    public void ClickIntroSave()
    {
        theSaveAndLoad.SaveData();
        CloseMenu();
        StartCoroutine(LoadWithDelay());
    }

    IEnumerator LoadWithDelay()
    {
        // 원하는 딜레이(예: 2초)만큼 기다린 후 씬 로드
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

        while (!SceneManager.LoadSceneAsync(sceneName).isDone)
        {
            yield return null;
        }
    }

    public void ClickIntro()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ClickNew()
    {
        SceneManager.LoadScene(newscene);
    }
}

