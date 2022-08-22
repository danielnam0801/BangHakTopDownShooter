using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemyAliveCnt;
    public bool _isSpawnEnemy;
    public bool _isSpawner;

    public bool _isSpawning;
    public bool _isAllEnemyDie;

    StageScript _stage;
    GameManager _gameManager;

    private void Awake()
    {
        _isSpawning = true;
        _isSpawner = true;
        _stage = GameObject.Find("Manager").GetComponent<StageScript>();
        _gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if(_isSpawnEnemy ==false && _isSpawner == false)
        {
            _isSpawning = false;
        }
        else
        {
            _isSpawning = true;
        }
        if(!_isSpawning && enemyAliveCnt == 0)
            _isAllEnemyDie = true;
        
        else if(_isSpawning && enemyAliveCnt == 0)
            _isAllEnemyDie = false;
        
        if(_gameManager._stageChange == false && _isAllEnemyDie)
        {
            
            _stage.Clear();
            Debug.Log("1");
            _isSpawning = true;
            _gameManager._stageChange = true;
        }
    }
}
