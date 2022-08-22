using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackPlayer : MonoBehaviour 
{
    [SerializeField]
    private List<FeedBack> _feedbackToPlay = null;

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }
    public void FinishFeedback()
    {
        foreach(FeedBack f in _feedbackToPlay)
        {
            f.CompletePrevFeedback();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
