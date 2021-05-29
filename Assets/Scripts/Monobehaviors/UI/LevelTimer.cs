using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeTxt;
    Coroutine countdownCO;
    WaitForSeconds oneSecond = new WaitForSeconds(1);
    TimeSpan timeSpan;

    public void SetTime(float timeSeconds)
    {
        timeSpan = TimeSpan.FromSeconds(timeSeconds);
        timeTxt.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        //CountDownTime();
    }
    public void SetTime(TimeSpan timeSpan)
    {
        timeTxt.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        this.timeSpan = timeSpan;
        //CountDownTime();
    }
    public void CountDownTime()
    {
        countdownCO = StartCoroutine(COCountDownTime());
    }

    private IEnumerator COCountDownTime()
    {
        while (timeSpan.TotalSeconds > 0)
        {
            yield return oneSecond;
            timeSpan = timeSpan.Subtract(TimeSpan.FromSeconds(1));
            SetTime(timeSpan);
        }
        GameManager.Instance.Lose();
    }
    public void Pause()
    {
        StopCoroutine(countdownCO);
    }
}
