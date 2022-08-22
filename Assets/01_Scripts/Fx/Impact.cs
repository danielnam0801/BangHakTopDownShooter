using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Impact : PoolAbleMono
{

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void DestroyAfterAnimation()
    {
        PoolManager.Instance.Push(this);
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos,rot);
        _audioSource.Play();
    }

    public void SetScaleAndTime(Vector3 scale, float time)
    {
        transform.localScale = scale;
        Invoke("DestroyAfterAnimation", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        
    }
}
