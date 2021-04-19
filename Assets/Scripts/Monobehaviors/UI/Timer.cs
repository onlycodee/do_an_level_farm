using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeTxt;
    Coroutine countdownCO;
    WaitForSeconds oneSecond = new WaitForSeconds(1);
    TimeSpan timeSpan;

    public void SetTime(TimeSpan timeSpan)
    {
        timeTxt.text = string.Format("{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds);
        this.timeSpan = timeSpan;
    }
    public void CountDownTime()
    {
        countdownCO = StartCoroutine(COCountDownTime());
    }

    private IEnumerator COCountDownTime()
    {
        yield return oneSecond;
        timeSpan.Subtract(TimeSpan.FromSeconds(1));
        SetTime(timeSpan);
    }
    public void Pause()
    {
        StopCoroutine(countdownCO);
    }
}
