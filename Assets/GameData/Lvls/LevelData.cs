using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct EnemySpawnEntry {
    public GameObject enemyPrefab;
    public int count;
    public float delayBetweenSpawns;
    public float delayBeforeStarting; 
}

[CreateAssetMenu(fileName = "Level_", menuName = "Levels/LevelData")]
public class LevelData : ScriptableObject {
    public int levelNumber;
    public List<EnemySpawnEntry> enemyGroups;
    public int goldReward;
}