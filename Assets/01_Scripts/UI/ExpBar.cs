using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Transform _ExpBar;

    public void SetExpBar(float value)
    {
        _ExpBar.localScale = new Vector3(value, 1, 1);
       
    }
}
