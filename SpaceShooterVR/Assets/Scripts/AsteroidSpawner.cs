using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField]
    private Vector3 _spawnArea = new Vector3(20, 30, 80);

    [SerializeField]
    private float _spawnRate = 1.0f;

    [SerializeField]
    private GameObject _asteroidPrefab;

    private float _spawnTimer = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawCube(transform.position, _spawnArea);
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer > _spawnRate)
        {
            _spawnTimer = 0.0f;
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        //Spawn Asteroid at random point in spawnarea
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1 * _spawnArea.x / 2, _spawnArea.x / 2), 
                                                                 Random.Range(-1 * _spawnArea.y / 2, _spawnArea.y / 2), 
                                                                 Random.Range(-1 * _spawnArea.z / 2, _spawnArea.z / 2));

        GameObject asteroid = Instantiate(_asteroidPrefab, spawnPosition, transform.rotation);

        asteroid.transform.parent = this.transform;
    }
}
