using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update() {

    } 

    //Spawn Enemy at random X position every 5 seconds
    IEnumerator SpawnRoutine() {
        while (_stopSpawning == false) {
            float randX = Random.Range(-9.5f, 9.5f);
            Vector3 positionToSpwan = new(randX, 7.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpwan, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
