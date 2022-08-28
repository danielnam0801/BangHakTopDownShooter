using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    private Transform _player;
    public Transform Player { get => _player; }

    [SerializeField]
    private PoolingListSO _poolingList;
    [SerializeField]
    private Texture2D _cursorTexture = null;

    [SerializeField]
    private StageDataAll _stageData;
    public int _currentStage = 0;
    EnemyManager _enemyManager;
    public bool _stageChange;

    
    WeaponDataSO _weaponData;

    [SerializeField]
    private float _criticalRate = 0.7f, _criticalMinDmg = 1.5f, _ciriticalMaxDmg = 2.5f;

    public bool IsCritical => Random.value < _weaponData.criticalRate;
    public float GetCriticalDamage(float dmg)
    {
        float ratio = _weaponData.criticalDmg;
        dmg = dmg * ratio;
        return dmg;
    }

    private void Awake()
    {
        _weaponData = GameObject.Find("G.U.N").GetComponent<Weapon>().WeaponData;    
        _currentStage = 0;
        if (instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        instance = this;

        PoolManager.Instance = new PoolManager(transform);
        CreatePool();
        SetCursorIcon();
        _enemyManager = GameObject.Find("Manager").GetComponent<EnemyManager>();
        //_stageData = GetComponent<StageDataAll>();
        _stageChange = false;
        StartCoroutine("StartBefore");
    }

    [SerializeField] float beforetime = 3f;
    IEnumerator StartBefore()
    {
        yield return new WaitForSeconds(beforetime);
        _stageChange = true;
    }
    private void CreatePool()
    {
        foreach (PoolingPair pp in _poolingList.list)
        {
            PoolManager.Instance.CreatePool(pp.prefab, pp.poolCount);
        }
    }

    private void SetCursorIcon()
    {
        Cursor.SetCursor(_cursorTexture,
            new Vector2(_cursorTexture.width / 2f, _cursorTexture.height / 2f),
            CursorMode.Auto);
    }

    private float _nextGenerationTime = 0f;
    [SerializeField]
    private float _generateMinTime = 4f, _generateMaxTime = 8f;

    private void Update()
    {
        if (_enemyManager._isSpawning == true && _stageChange)
        {
            SpawnTime(_stageData._list[0].stageData[_currentStage].spawnSpawnerCnt, _stageData._list[0].stageData[_currentStage].spawnEnemyCnt);
            _stageChange = false;
        }
    }
    public void SpawnTime(int _SpawnCnt, int _EnemyCount)
    {
        StartCoroutine(GameLoop(_SpawnCnt, _EnemyCount));
        _enemyManager._isSpawner = true;
        Debug.Log("currentStage : " + _currentStage);
    }
    IEnumerator GameLoop(int _Spawncnt, int _EnemyCnt)
    {
        int i = 0;
        for (i = 0; i < _Spawncnt; i++)
        {
            yield return new WaitForSeconds(_nextGenerationTime);

            float posX = Random.Range(-4.5f, 4.5f);
            float posY = Random.Range(-5f, 5f);
            Spawner spawner = PoolManager.Instance.Pop("Spawner") as Spawner;
            spawner.transform.position = new Vector3(posX, posY);
            spawner.StartToSpawn(_EnemyCnt, _currentStage);
            _nextGenerationTime = Random.Range(_generateMinTime, _generateMaxTime);
        }
        Debug.Log("Á¤»ó");
        if(i == _Spawncnt)
        {
            _enemyManager._isSpawner = false;
            _currentStage++;
        }
       
        

    }
}
            
    