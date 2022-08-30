using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeFeedBack : FeedBack
{
    [SerializeField]
    private float _beforeFreezeDelay = 0.05f, _unFreezeTimeDelay = 0.02f;

    [SerializeField]
    [Range(0f, 1f)]
    private float _timeFreezeValue = 0.2f;
    public override void CompletePrevFeedback()
    {
        TimeController.instance?.ResetTimeScale();
    }

    public override void CreateFeedback()
    {
        if(TimeController.instance.isActiveTime == false)
        {
            TimeController.instance.isActiveTime = true;
            TimeController.instance?.ModifyTimeScale(_timeFreezeValue, _beforeFreezeDelay, () =>
            {
                TimeController.instance?.ModifyTimeScale(1, _unFreezeTimeDelay);
                TimeController.instance.isActiveTime = false;
            });
        }
       
    }
}

