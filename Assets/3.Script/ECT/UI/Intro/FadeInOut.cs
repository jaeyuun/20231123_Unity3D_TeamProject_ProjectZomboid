using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup logoImage;
    [SerializeField] private CanvasGroup titleImage;
    private float timeToFade = 0.4f;


    private void Awake()
    {
        StartCoroutine(TitleFadeInOut_Co());
    }

    private void Update()
    {
        LogoFadeInOut();
    }

    private void LogoFadeInOut()
    {
        logoImage.alpha += 0.4f * Time.deltaTime;
    }

    private IEnumerator TitleFadeInOut_Co()
    {
        yield return new WaitForSeconds(4f);
        titleImage.alpha = 0;
        yield return new WaitForSeconds(0.2f);
        titleImage.alpha = 1;
        yield return new WaitForSeconds(0.2f);
        titleImage.alpha = 0;
        yield return new WaitForSeconds(0.2f);
        titleImage.alpha = 1;
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            titleImage.alpha -= 1f * Time.deltaTime;
            yield return null;
            if (titleImage.alpha.Equals(1)) break;
        }
        StartCoroutine(TitleFadeInOut_Co());
    }
}
