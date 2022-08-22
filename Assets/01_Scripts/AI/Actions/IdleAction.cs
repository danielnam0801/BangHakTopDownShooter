using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{

    public override void TakeAction()
    {
        _aIMovementData.direction = Vector2.zero;
        _aIMovementData.pointOfInterest = transform.position;
        _brain.Move(_aIMovementData.direction, _aIMovementData.pointOfInterest);
    }
}
