using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Agent/Movement")]
public class MovementDataSO : ScriptableObject
{
    [Range(1,10)]
    public float maxSpeed = 5;

    [Range(0.1f,100f)]
    public float accerlation = 50, deAccerlation = 50;

}
