using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "PJunYeong";
    public string loadName = "GameLoad";



    public void ClickStart()
    {
        Debug.Log("로딩 ");
        
        SceneManager.LoadScene(sceneName);
       
    }
    public void ClickLoad()
    {
        Debug.Log("로드 ");


        SceneManager.LoadScene(loadName);
       
      
    }

    public void ClickExit()
    {
        Debug.Log("종료");
        Application.Quit();
    }
   
}
