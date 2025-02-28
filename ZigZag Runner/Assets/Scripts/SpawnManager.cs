using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool _gameOver = false;
    [SerializeField] private float _spawnInterval = 0.25f;
    [SerializeField] private GameObject _platformPrefab; // Add Platform Prefab in Edit Mode
    private Vector3 _oldPlatformPosition;
    private Vector3 _platformPosition;
    private float _size;

    void Start()
    {
        _oldPlatformPosition = transform.position;
        if (_platformPrefab == null) {
            Debug.LogError("SpawnManager.cs::Start() - Platform Prefab Not Found");
        }
        _size = _platformPrefab.transform.localScale.x;
        StartCoroutine(SpawnPlatforms());
    }

    IEnumerator SpawnPlatforms() {
        while (!_gameOver) {
            Vector3 offset = Random.Range(0, 2) == 0 ? new(_size, 0, 0) : new(0, 0, _size);
            _platformPosition = _oldPlatformPosition + offset;
            Instantiate(_platformPrefab, _platformPosition, Quaternion.identity);
            _oldPlatformPosition = _platformPosition;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void GameOver() {
        _gameOver = true;
    }
}
