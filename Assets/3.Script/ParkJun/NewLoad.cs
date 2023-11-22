using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewLoad : MonoBehaviour
{
    private string sceneName = "MainGame_Fake";
    public Text text1;
    public Text text2;
    public Button continueButton;
 

    private void Start()
    {
        StartCoroutine(ShowText1());
    }

    IEnumerator ShowText1()
    {
        text1.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // 텍스트 1을 3초 동안 보여줌

        text1.gameObject.SetActive(false); // 텍스트 1을 비활성화
        StartCoroutine(ShowText2());
    }

    IEnumerator ShowText2()
    {
        text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // 텍스트 2를 3초 동안 보여줌

        text2.gameObject.SetActive(false); // 텍스트 2를 비활성화
        continueButton.gameObject.SetActive(true); // 버튼 활성화
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
