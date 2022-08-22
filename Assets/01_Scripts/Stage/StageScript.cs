using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class StageScript : MonoBehaviour
{
    public UnityEvent OnClearStage;
    // Start is called before the first frame update
    [SerializeField]
    private CinemachineVirtualCamera _cmVcam;
    /// <summary>
    /// cam쪽에서 오류생길것같음 이런경우 캠을 하나더 만들어야함
    /// </summary>
    [SerializeField]
    private float _amplitude = 1, _frequency = 1;
    [SerializeField]
    private float _duration = 0.1f;
    [SerializeField]
    private GameObject _Room;
    private CinemachineBasicMultiChannelPerlin _noise;

    [SerializeField] float xMove;
    [SerializeField] float roomMoveSpeed;
    [SerializeField] Ease roomMoveEase;

    private void OnEnable()
    {
        _cmVcam = Define.VCam;
        _noise = _cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Clear()
    {
        OnClearStage.Invoke();
    }

    //public override void CompletePrevFeedback()
    //{
    //    StopAllCoroutines();
    //    _noise.m_AmplitudeGain = 0;
    //}
    //public override void CreateFeedback()
    //{
    //    _noise.m_AmplitudeGain = _amplitude;
    //    _noise.m_FrequencyGain = _frequency;
    //    StartCoroutine(ShakeCoroutine());
    //}

    IEnumerator ShakeCoroutine()
    {
       float time = _duration;
        while (time/2 > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(_amplitude, 0, time / _duration);
            yield return null;
            time -= Time.deltaTime; 
        }
        
        time = _duration;
        while(time/2 > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0,_amplitude,time / _duration);
            yield return null;
            time -= Time.deltaTime;
        }
        _noise.m_AmplitudeGain = 0;
        yield return null;
    }
    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void RoomMove()
    {
        xMove += xMove;
        StartCoroutine(RMove());
    }
    IEnumerator RMove()
    {
        yield return new WaitForSeconds(1.5f);
        Vector3 targetXpos = transform.position + new Vector3(xMove,0,0);
        _Room.transform.DOMove(targetXpos, roomMoveSpeed, true).SetEase(roomMoveEase);
        
    }

 


}
