using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAIBrain _brain = null;
    [SerializeField]
    public List<AIAction> _acitons = null;
    [SerializeField]
    public List<AITransition> _transition = null;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<EnemyAIBrain>();
    }

    public void UpdateState()
    {
        foreach(AIAction a in _acitons)
        {
            a.TakeAction();
        }

        foreach(AITransition tr in _transition)
        {
            bool result = false;
            foreach(AIDecision d in tr.decisions)
            {
                result = d.MakeADecision();
                if (result == false) break;
            }
            
            if (result == true)
            {
                if(tr.positiveState != null)
                {
                    _brain.ChangeSstate(tr.positiveState);
                    return;
                }
            }
            else
            {
                if(tr.negativeState != null)
                {
                    _brain.ChangeSstate(tr.negativeState);
                    return;
                }
            }
        }


    }
}
