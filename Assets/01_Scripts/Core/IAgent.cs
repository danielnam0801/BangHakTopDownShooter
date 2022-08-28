using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IAgent
{
    public float Health { get; }
    public UnityEvent OnDie { get; set; }
    public UnityEvent OnGetHit { get; set; }
}
