using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        if(_aiActionData.isAttack == true)
        {
            _aiActionData.isAttack = false;
        }

        Vector2 direction = _brain.Target.position - transform.position;
        _aIMovementData.direction = direction.normalized;
        _aIMovementData.pointOfInterest = _brain.Target.position;

        _brain.Move(direction.normalized, _brain.Target.position);
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
