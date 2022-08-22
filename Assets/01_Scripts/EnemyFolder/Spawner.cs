using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Spawner : PoolAbleMono
{
    [SerializeField]
    private List<EnemyDataSO> _spawnEnemies = null;

    private Light2D _spawnLight;
    [SerializeField]
    private float _targetIntensity = 1.5f;
    [SerializeField]
    [Range(0, 4f)]
    private float _radius = 3f;
    [SerializeField]
    private float _delayMin = 0.5f, _delayMax = 1.5f;

    private EnemyManager _manager;


    private void Awake()
    {
        _spawnLight = GetComponent<Light2D>();
        _spawnLight.intensity = 0;
        _manager = GameObject.Find("Manager").GetComponent<EnemyManager>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P)) //????? ???
        //{
        //    StartToSpawn(5);
        //}
    }

    public void StartToSpawn(int count)
    {
        _manager._isSpawnEnemy = true;
        Tween lightTween = DOTween.To(
            () => _spawnLight.intensity,
            x => _spawnLight.intensity = x,
            _targetIntensity,
            2f);

        Sequence seq = DOTween.Sequence();
        seq.Append(lightTween);
        seq.AppendCallback(() => StartCoroutine(SpawnCoroutine(count)));
    }

    IEnumerator SpawnCoroutine(int count)
    {
        int i = 0;
        for (i = 0; i < count; i++)
        {
            
            float delay = Random.Range(_delayMin, _delayMax);
            int index = Random.Range(0, _spawnEnemies.Count);

            yield return new WaitForSeconds(delay);
            EnemyDataSO target = _spawnEnemies[index];

            Vector3 position = (Vector2)transform.position + Random.insideUnitCircle * _radius;
            Enemy enemy = PoolManager.Instance.Pop(target.prefab.name) as Enemy;
            _manager.enemyAliveCnt++;
            Debug.Log(enemy);
            enemy.transform.SetPositionAndRotation(position, Quaternion.identity);

            //Enemy enemy = Instantiate(target.prefab, position, Quaternion.identity).GetComponent<Enemy>();

            enemy.Spawn();
            
        }
        DOTween.To(
            () => _spawnLight.intensity,
            x => _spawnLight.intensity = x,
            0,
            2f).OnComplete(() => {
                PoolManager.Instance.Push(this);
                
            });
    _manager._isSpawnEnemy = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.color = Color.white;
        }
    }

    public override void Init()
    {
        // do NOTING
    }
#endif
}
