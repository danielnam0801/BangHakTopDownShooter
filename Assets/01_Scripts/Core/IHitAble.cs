using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitAble
{
    public bool IsEnemy { get; }//메서드도 아니고 변수도 아닌 프로퍼티; 
    public Vector3 HitPoint { get; }
    public void GetHit(int damage, GameObject damageDealer);

}
