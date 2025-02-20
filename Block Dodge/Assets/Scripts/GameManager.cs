using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cratePrefab; //Assigned in edit mode

    [SerializeField]
    private bool _isPlayerAlive = true;

    private float _spawnRate = 1.0f;
    private int _score = 0;
    private TextMeshProUGUI _scoreText;
    private int _highscore;

    void Start()
    {
        if (_cratePrefab == null)
        {
            Debug.LogError("GameManager.cs::Start() - Crate Prefab Not attached.");
        }
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(SpawnCrates());
        _highscore = PlayerPrefs.GetInt("highscore", _highscore);
    }

    void Update()
    {

    }

    IEnumerator SpawnCrates()
    {
        yield return new WaitForSeconds(1);
        while (_isPlayerAlive)
        {
            Vector2 randX = new(Random.Range(-2.2f, 2.2f), 5.8f);
            Instantiate(_cratePrefab, randX, Quaternion.identity);
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void PlayerDead()
    {
        _isPlayerAlive = false;
    }

    public void UpdateScore()
    {
        if (_isPlayerAlive)
        {
            _score++;
            _scoreText.text = _score.ToString();
            if (_score > _highscore) {
                _highscore = _score;
            }
        }
        if (!_isPlayerAlive) {
            PlayerPrefs.SetInt("highscore", _highscore);
        }
    }
}
