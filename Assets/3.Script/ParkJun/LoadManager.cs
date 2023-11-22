using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    private string sceneName = "MainGame_Fake";
    private GameObject canvas;
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
            instance.Find_teset();
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (canvas == null && SceneManager.GetActiveScene().name.Equals("GameLoad"))
        {
            canvas = GameObject.Find("Canvas");
        }
        StartCoroutine(ShowText1());
    }

    public void Find_teset() {
        if (canvas == null && SceneManager.GetActiveScene().name.Equals("GameLoad"))
        {
            canvas = GameObject.Find("Canvas");
        }
        StartCoroutine(ShowText1());
    }
    private void FindObject()
    {
        // Button Event add
        if (canvas != null)
        {
            continueButton = canvas.transform.GetChild(3).gameObject.transform.GetComponent<Button>();
            // button addListener
            continueButton.onClick.AddListener(ContinueToNextScene);
        }
    }

    IEnumerator ShowText1()
    {
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // 텍스트 1을 3초 동안 보여줌

        canvas.transform.GetChild(1).gameObject.SetActive(false); // 텍스트 1을 비활성화
        StartCoroutine(ShowText2());
    }

    IEnumerator ShowText2()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // 텍스트 2를 3초 동안 보여줌

        canvas.transform.GetChild(2).gameObject.SetActive(false); // 텍스트 2를 비활성화
        canvas.transform.GetChild(3).gameObject.SetActive(true); // 버튼 활성화
        FindObject();
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
    }
}
