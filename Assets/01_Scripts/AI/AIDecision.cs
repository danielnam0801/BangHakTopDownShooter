using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aIMovementData;

    protected EnemyAIBrain _brain;

    protected virtual void Awake()
    {
        _brain = transform.parent.parent.parent.GetComponent<EnemyAIBrain>();
        _aiActionData = _brain.transform.Find("AI").GetComponent<AIActionData>();
        _aIMovementData = _brain.transform.Find("AI").GetComponent<AIMovementData>();
    }

    public abstract bool MakeADecision();

}
