using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class StageData
{
    public int spawnSpawnerCnt;
    public int spawnEnemyCnt;
    public List<EnemyDataSO> spawnEnemyPrefab;
}

[CreateAssetMenu(menuName = "SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public List<StageData> stageData;
}