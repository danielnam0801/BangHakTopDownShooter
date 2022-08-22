using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WEAPON/WeaponData")]
public class WeaponDataSO : ScriptableObject
{

    public BulletdataSO bulletdata;

    [Range(0, 999)] public int ammoCapacity = 100;
    public bool automaticFire;
    [Range(0.1f, 2f)] public float weaponDelay = 0.1f;
    [Range(0, 10f)] public float spreadAngle = 5f;

    [SerializeField] private bool _multiBulletShot = false;
    [SerializeField] private int _bulletCount = 1;

    [Range(0.1f, 2f)] public float reloadTime = 1f;

    public AudioClip shootClip;
    public AudioClip outOfAmmoClip;
    public AudioClip reloadClip;

    public int GetBulletCountToSpawn()
    {
        return  _multiBulletShot ? _bulletCount : 1;
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
