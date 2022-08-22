using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WEAPON/BulletData")]
public class BulletdataSO : ScriptableObject 

{
    public GameObject bulletPrefab;
    [Range(1, 10)] public int damage = 1;
    [Range(1, 100)] public float bulletSpeed = 1;

    public GameObject impactObstaclePrefab;//장애물 충돌시 효과
    public GameObject impactEnemyPrefab;//플레이어에 부딛혔을때 효과

    public float lifeTime = 2f;

}
