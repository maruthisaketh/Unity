using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject[] _enemiesPrefabs;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerupPrefabs;

    private bool _stopSpawning = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    //Spawn Enemy at random X position every 5 seconds
    IEnumerator SpawnEnemyRoutine() {
        while (_stopSpawning == false) {
            float randX = Random.Range(-9.5f, 9.5f);
            Vector3 positionToSpawn = new(randX, 7.0f, 0);
            int randomEnemy = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_enemiesPrefabs[randomEnemy], positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.5f);
        }
    }

    IEnumerator SpawnPowerupRoutine() {
        while (_stopSpawning == false) {
            Vector3 positionToSpawn = new(Random.Range(-9.5f, 9.5f), 7.0f, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupPrefabs[randomPowerup], positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8.0f, 12.0f));
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
