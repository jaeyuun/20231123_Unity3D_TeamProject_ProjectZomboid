using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    public Text textToBlink;
    public float blinkInterval = 0.5f;

    void Start()
    {
        // 코루틴을 시작합니다.
        StartCoroutine(Blink_Text());
    }

    IEnumerator Blink_Text()
    {
        while (true)
        {
            // 텍스트를 활성화 또는 비활성화합니다.
            textToBlink.enabled = !textToBlink.enabled;

            // 지정된 간격만큼 기다립니다.
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
