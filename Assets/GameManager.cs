using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public List<LevelData> allLevels;
    private int currentLevelIndex = 0;

    private int totalEnemiesToSpawn;
    private int enemiesSpawnedSoFar;
    private int enemiesKilledSoFar;

    public Transform spawnPoint;
    public GameObject upgradeUI;

    public EntitiesManager em;

    void Start() {
        StartLevel(currentLevelIndex);
        em = FindAnyObjectByType<EntitiesManager>().GetComponent<EntitiesManager>();
    }

    void StartLevel(int index) {
        if (index >= allLevels.Count) return;

        upgradeUI.SetActive(false);
        Time.timeScale = 1f;

        enemiesSpawnedSoFar = 0;
        enemiesKilledSoFar = 0;
        totalEnemiesToSpawn = CalculateTotalEnemies(allLevels[index]);

        StartCoroutine(SpawnLevelRoutine(allLevels[index]));
    }

    int CalculateTotalEnemies(LevelData level) {
        int total = 0;
        foreach (var group in level.enemyGroups) total += group.count;
        return total;
    }

    IEnumerator SpawnLevelRoutine(LevelData level) {
        foreach (var group in level.enemyGroups) {
            yield return new WaitForSeconds(group.delayBeforeStarting);

            for (int i = 0; i < group.count; i++) {
                var enemy = Instantiate(group.enemyPrefab, spawnPoint.position, Quaternion.identity);
                em.enemies.Add(enemy);
                enemiesSpawnedSoFar++;

                yield return new WaitForSeconds(group.delayBetweenSpawns);
            }
        }
    }

    public void EnemyDied() {
        enemiesKilledSoFar++;
        if (enemiesKilledSoFar >= totalEnemiesToSpawn) {
            EndLevel();
        }
    }

    void EndLevel() {
        Time.timeScale = 0f;
        upgradeUI.SetActive(true);
    }

    public void OnUpgradeSelected() {
        currentLevelIndex++;
        StartLevel(currentLevelIndex);
    }
}