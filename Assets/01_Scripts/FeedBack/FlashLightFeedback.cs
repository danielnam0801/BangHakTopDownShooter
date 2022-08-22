using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLightFeedback : FeedBack
{
    [SerializeField]
    private Light2D _lightTarget = null;
    [SerializeField]
    private float _lightOnDelay = 0.01f, _lightOffDelay = 0.01f;
    [SerializeField]
    private bool _defaultState = false;
    public override void CompletePrevFeedback()
    {
        StopAllCoroutines();
        _lightTarget.enabled = false;
    }

    IEnumerator ToggleLightCo(float time, bool result, Action Callback = null)
    {
        yield return new WaitForSeconds(time);
        _lightTarget.enabled = result;
        Callback?.Invoke();
    }

    public override void CreateFeedback()
    {
        StartCoroutine(ToggleLightCo(_lightOnDelay, true, () =>{
            StartCoroutine(ToggleLightCo(_lightOffDelay, false));
        }));
    }
}