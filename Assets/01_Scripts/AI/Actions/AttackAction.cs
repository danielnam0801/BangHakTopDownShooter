using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _aIMovementData.direction = Vector2.zero;
        if(_aiActionData.isAttack == false)
        {
            _brain.Attack();
            _aIMovementData.pointOfInterest = _brain.Target.position;
        }
        _brain.Move(_aIMovementData.direction, _aIMovementData.pointOfInterest);
    }
}
