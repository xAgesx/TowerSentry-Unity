using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public List<LevelData> allLevels;
    private int currentLevelIndex = 0;

    private int totalEnemiesToSpawn;
    private int enemiesSpawnedSoFar;
    public int enemiesKilledSoFar;

    public Transform spawnPoint;
    public GameObject upgradeUI;

    public EntitiesManager em;
    public GameObject statsUI;
    private float radius;
    public float[] extremes ;

    void Awake() {
        instance = this;
    }
    void Start() {
        StartLevel(currentLevelIndex);

        em = FindAnyObjectByType<EntitiesManager>().GetComponent<EntitiesManager>();
        statsUI = GameObject.Find("EntityStats");
        statsUI.SetActive(false);
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
                float angle = Random.Range(0f, System.MathF.PI * 2);
                radius = Random.Range(extremes[0], extremes[1]);
                
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                Vector3 spawnPosition = new Vector3(x, 0, z) ;
                


                var enemy = Instantiate(group.enemyPrefab, spawnPosition, Quaternion.identity);
                em.enemies.Add(enemy);
                enemiesSpawnedSoFar++;

                yield return new WaitForSeconds(group.delayBetweenSpawns);
            }
        }
    }

    public void EnemyDied() {
        enemiesKilledSoFar++;
        if (enemiesKilledSoFar >= totalEnemiesToSpawn) {
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel() {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0f;
        upgradeUI.SetActive(true);
    }

    public void OnUpgradeSelected() {
        currentLevelIndex++;
        StartLevel(currentLevelIndex);
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.zero,extremes[0]);
        Gizmos.DrawWireSphere(Vector3.zero,extremes[1]);
    }
}