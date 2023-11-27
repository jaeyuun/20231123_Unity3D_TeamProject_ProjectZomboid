using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldTime;

public class GameOverText : MonoBehaviour
{
    [SerializeField] WorldTimeScript worldTimeScript;
    [SerializeField] private Text gameOverText;
    private TimeSpan _currentTime;
    private string timeText = string.Empty;

    private void Update()
    {
        SurTimeText();
    }

    private void SurTimeText()
    {
        _currentTime = worldTimeScript._currentTime;
        timeText = _currentTime.ToString(@"dd");
        if (timeText[0].Equals("0"))
        {
            timeText = timeText[1].ToString();
        }
        gameOverText.text = $"당신은 {timeText}일 동안 생존하였습니다.";
    }
}
