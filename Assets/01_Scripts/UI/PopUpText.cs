using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : PoolAbleMono
{ 
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void SetUp(float damageAmount, Vector3 pos, bool isCritical, Color color)
    {
        transform.position = pos;
        _textMesh.SetText(damageAmount.ToString());

        if(isCritical == true)
        {
            _textMesh.color = Color.red;
            _textMesh.fontSize = 10f;
        }
        else
        {
            _textMesh.color = color;
        }
        ShowingSequence();
    }

    private void ShowingSequence()
    {
        Sequence seq = DOTween.Sequence();
        Vector3 targetPosition = transform.position + new Vector3(0.5f,0,0);
        seq.Append(transform.DOJump(targetPosition, 1.5f, 1, 1f));
        seq.Join(_textMesh.DOFade(0, 1f));//이상하면 여기 부분
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }

    public override void Init()
    {
        _textMesh.color = Color.white;
        _textMesh.fontSize = 7f;
    }
}
