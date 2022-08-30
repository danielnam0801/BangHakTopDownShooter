using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public static TimeController instance;

    public bool isActiveTime = false;
    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }
    public void ResetTimeScale()
    {
        //Debug.Log("!!!RESETED!!!");
        StopAllCoroutines();
        Time.timeScale = 1f;
    }
    public void ModifyTimeScale(float targetValue, float timeToWait, Action OnComplete = null)
    {
        StartCoroutine(TimeScaleCoroutine(targetValue, timeToWait, OnComplete));
        //Debug.Log("!!!CHANGE COMPLETED!!!");
    }
    IEnumerator TimeScaleCoroutine(float targetValue, float timeToWait, Action OnComplete = null)
    {
        yield return new WaitForSecondsRealtime(timeToWait); //timeScale¿¡ ¿µÇâ x
        Time.timeScale = targetValue;
        OnComplete?.Invoke();
    }
}
