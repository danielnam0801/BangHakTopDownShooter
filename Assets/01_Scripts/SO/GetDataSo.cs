using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    None,
    Weapon,
    Agent,
    Bullet
}

public enum DataTypeDetail
{
    capacity,
    AttackMode,
    AttackSpeed,
    BulletSpeed,
    CriticalDamage,
    CriticalRate,
    CriticalRate2,
    Damage,
    PlayerMoveSpeed,
    ReloadSpeed

}

[CreateAssetMenu(menuName = "SO/GetData")]
public class GetDataSo : ScriptableObject
{
    public GameObject buff;
    public DataType dataType;
    public DataTypeDetail detail;       
}
