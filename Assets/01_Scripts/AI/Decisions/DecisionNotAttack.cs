using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionNotAttack : AIDecision
{
    public override bool MakeADecision()
    {
        return !_aiActionData.isAttack;
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
