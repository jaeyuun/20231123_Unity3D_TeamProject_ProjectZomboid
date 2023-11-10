using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "PJunYeong";

    public static Title instance;

    private SaveAndLoad theSaveAndLoad;

    //싱글톤 씬 이동간에 파괴되지 않게 
    private void Awake()
    {
       

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        Debug.Log("로딩 ");
        gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
        // "GameTitle"의 Canvas는 잠시 비활성화
    }
    public void ClickLoad()
    {
        Debug.Log("로드 ");

       
        StartCoroutine(LoadCor());
      
    }
    IEnumerator LoadCor()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) //로딩이 끝나지 않는다면 
        {
            yield return null;
        }
        
        theSaveAndLoad = FindObjectOfType<SaveAndLoad>();
        theSaveAndLoad.LoadData();
        gameObject.SetActive(false);  // "GameTitle"의 Canvas는 잠시 비활성화

    }
    public void ClickExit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
   
}
