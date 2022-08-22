using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCinemachineFeedback : FeedBack
{
    // Start is called before the first frame update
    [SerializeField]
    private CinemachineVirtualCamera _cmVcam;
    [SerializeField]
    [Range(0f, 5f)]
    private float _amplitude = 1, _frequency = 1;
    [SerializeField]
    [Range(0f, 1f)]
    private float _duration = 0.1f;

    private CinemachineBasicMultiChannelPerlin _noise;
    
    private void OnEnable()
    {
        _cmVcam = Define.VCam;
        _noise = _cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CompletePrevFeedback()
    {
        StopAllCoroutines();
        _noise.m_AmplitudeGain = 0;
    }
    public override void CreateFeedback()
    {
        _noise.m_AmplitudeGain = _amplitude;
        _noise.m_FrequencyGain = _frequency;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float time = _duration;
        while (time > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / _duration);
            yield return null;
            time -= Time.deltaTime;
        }
        _noise.m_AmplitudeGain = 0;
        yield return null;
    }
}
