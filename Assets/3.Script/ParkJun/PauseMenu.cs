using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private SaveAndLoad theSaveAndLoad;
    public string sceneName = "TestIntro_UIFix Test";

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
        CloseMenu();
        theSaveAndLoad.SaveData();
     
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
}

