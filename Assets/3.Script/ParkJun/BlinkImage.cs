using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public Image ImageToBlink;
    public float blinkInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkImageRoutine());
    }

    // 깜빡거리는 루틴
    IEnumerator BlinkImageRoutine()
    {
        while (true)
        {
            // 이미지를 활성화 또는 비활성화합니다.
            ImageToBlink.enabled = !ImageToBlink.enabled;

            // 지정된 간격만큼 기다립니다.
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
