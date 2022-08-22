using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedBack : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void CompletePrevFeedback();

    protected virtual void OnDestroy()
    {
        CompletePrevFeedback();
    }

    protected virtual void OnDisable()
    {
        CompletePrevFeedback();
    }
}
