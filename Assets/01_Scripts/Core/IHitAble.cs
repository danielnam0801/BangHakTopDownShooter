using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitAble
{
    public bool IsEnemy { get; }//�޼��嵵 �ƴϰ� ������ �ƴ� ������Ƽ; 
    public Vector3 HitPoint { get; }
    public void GetHit(int damage, GameObject damageDealer);

}
