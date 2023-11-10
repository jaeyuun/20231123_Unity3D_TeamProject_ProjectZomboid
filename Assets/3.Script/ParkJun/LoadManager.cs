using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public string sceneName = "PJunYeong";
    public Text text1;
    public Text text2;
    public Button continueButton;
    private AsyncOperation operation;
    private SaveAndLoad thesaveAndLoad;

    public static LoadManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    // 버튼에 연결하여 호출될 함수
    public void ContinueToNextScene()
    {
        StartCoroutine(LoadCoroutine());
    }
  

    IEnumerator LoadCoroutine()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                timer = 0f;
            }
            else
            {
                operation.allowSceneActivation = true;
            }
        }

        thesaveAndLoad = FindObjectOfType<SaveAndLoad>(); // 다음 씬의 SaveAndLoad
        thesaveAndLoad.LoadData();
        gameObject.SetActive(false);
     
    }
}
