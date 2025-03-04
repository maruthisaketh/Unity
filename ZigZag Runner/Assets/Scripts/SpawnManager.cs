using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool _gameOver = false;
    [SerializeField] private float _spawnInterval = 0.3f;
    [SerializeField] private GameObject _platformPrefab; // Add Platform Prefab in Edit Mode
    [SerializeField] private GameObject _platformContainer; // Add Platform container in Edit Mode
    [SerializeField] private GameObject _diamondPrefab; // Add Diamond Prefab in Edit Mode
    [SerializeField] private GameObject _diamondContainer; // Add Diamond container in Edit Mode
    private Vector3 _oldPlatformPosition;
    private Vector3 _platformPosition;
    private float _size;

    void Start()
    {
        _oldPlatformPosition = transform.position;
        if (_platformPrefab == null) {
            Debug.LogError("SpawnManager.cs::Start() - Platform Prefab Not Found");
        }
        if (_platformContainer == null) {
            Debug.LogError("SpawnManager.cs::Start() - Platform Container Not Found");
        }
        if (_diamondPrefab == null) {
            Debug.LogError("SpawnManager.cs::Start() - Diamond Prefab Not Found");
        }
        if (_diamondContainer == null) {
            Debug.LogError("SpawnManager.cs::Start() - Diamond container Not Found");
        }
        _size = _platformPrefab.transform.localScale.x;
    }

    IEnumerator SpawnItems() {
        while (!_gameOver) {
            int randomNum = Random.Range(0, 8);
            Vector3 offset = (randomNum % 2) == 0 ? new(_size, 0, 0) : new(0, 0, _size);
            _platformPosition = _oldPlatformPosition + offset;
            GameObject platform = Instantiate(_platformPrefab, _platformPosition, Quaternion.identity);
            platform.transform.parent = _platformContainer.transform;
            
            if (randomNum < 4) {
                GameObject diamond = Instantiate(_diamondPrefab, _platformPosition + Vector3.up, _diamondPrefab.transform.rotation);
                diamond.transform.parent= _diamondContainer.transform;
            }

            _oldPlatformPosition = _platformPosition;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void GameOver() {
        _gameOver = true;
    }

    public void StartGame() {
        StartCoroutine(SpawnItems());
    }
}
