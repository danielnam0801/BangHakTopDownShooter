using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    protected AudioSource _audioSource;
    
    [SerializeField]
    protected float _pitchRandomes = 0.25f;

    protected float _basePitch;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
        _basePitch = _audioSource.pitch;
    }

    protected void PlayClipWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-_pitchRandomes, +_pitchRandomes);
        _audioSource.pitch = _basePitch + randomPitch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
