using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    #region 발사관련로직
    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool _isShooting = false;
    protected bool _delayCoroutine = false;
    #endregion

    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected GameObject _muzzle;
    [SerializeField] protected TrackedReference _shellEjectPos;

    public WeaponDataSO WeaponData { get => _weaponData; }
    #region  Ammo 관련 코드
    public UnityEvent<int> OnAmmoChange;
    [SerializeField] protected int _ammo;
    public int Ammo
    {
        get => _ammo;
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
            OnAmmoChange?.Invoke(_ammo);
        }
    }
    public bool AmmoFull { get => Ammo == _weaponData.ammoCapacity; }
    public int EmptyBulletCnt { get => _weaponData.ammoCapacity - _ammo; }
    #endregion

    public UnityEvent OnPlayNoAmmoSound;
    public UnityEvent OnPlayReloadSound;
    
    void Start()
    {
        //변경
        Ammo = _weaponData.ammoCapacity;
        WeaponAudio wa = transform.Find("WeaponAudio").GetComponent<WeaponAudio>();
        wa.SetAudioClip(_weaponData.shootClip,
                        _weaponData.outOfAmmoClip,
                        _weaponData.reloadClip);

    }

    // Update is called once per frame
    void Update()
    {
        UseWeapon();    
    }

    private void UseWeapon()
    {
        //마우스 클릭중이고, 총의 딜레이가 false면 발사
        if(_isShooting == true && _delayCoroutine == false)
        {
            if(Ammo > 0)
            {
                Ammo -= _weaponData.GetBulletCountToSpawn();

                OnShoot?.Invoke();
                for(int i=0; i< _weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                _isShooting=false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    protected void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(_weaponData.automaticFire == false)
        {
            _isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCoroutine = false;
    }
    private void ShootBullet()
    {
        SpawnBullet(_muzzle.transform.position, CalculateAngle() , false);

    }

    private Quaternion CalculateAngle()
    {
        float spread = Random.Range(-_weaponData.spreadAngle, +_weaponData.spreadAngle);
        Quaternion spreadRot = Quaternion.Euler(new Vector3(0, 0, spread));
        return _muzzle.transform.rotation * spreadRot;
    }

    private void SpawnBullet(Vector3 position, Quaternion rot, bool isEnemyBullet)
    {
        //Bullet bullet = Instantiate(_weaponData.bulletdata.bulletPrefab).GetComponent<Bullet>();
        Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
        bullet.SetPositionAndRotation(position, rot);
        bullet.isEnemy = isEnemyBullet;
        bullet.BulletData = _weaponData.bulletdata;
        
    } 

    public void TryShooting()
    {
        _isShooting=true;
    }
    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }
    public void PlayReloadSound()
    {
        OnPlayReloadSound?.Invoke();
    }

    public void PlayCannotSound()
    {
        OnPlayNoAmmoSound?.Invoke();
    }
}
