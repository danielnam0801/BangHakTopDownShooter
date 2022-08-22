using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class StageData
{
    public int spawnSpawnerCnt;
    public int spawnEnemyCnt;
    public List<GameObject> spawnSpawnerPrefab;
    public List<GameObject> spawnEnemyPrefab;

}
[CreateAssetMenu(menuName = "SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public List<StageData> list;
}