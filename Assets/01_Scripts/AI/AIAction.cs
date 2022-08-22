using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aIMovementData;

    protected EnemyAIBrain _brain;

    protected virtual void Awake()
    {
        _brain = transform.parent.parent.GetComponent<EnemyAIBrain>();
        _aiActionData = transform.parent.GetComponent<AIActionData>();
        _aIMovementData = transform.parent.GetComponent<AIMovementData>();
    }

    public abstract void TakeAction();
}
