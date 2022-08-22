using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudionPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip _stepClip;

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(_stepClip);
    }
    
}
