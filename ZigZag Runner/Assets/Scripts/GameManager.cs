using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _zigzagText;
    private TextMeshProUGUI _tapToStartText;
    private int _score = 0;
    private int _highscore = 0;
    private bool _gameOver = false;
    private bool _gameStarted = false;
    private SpawnManager _spawnManager;

    private void Awake() {
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        if (_scoreText == null) {
            Debug.LogError("GameManager.cs::Start() - Score Text Component Not Found");
        }
        _highscore = PlayerPrefs.GetInt("highscore", 0);
        _scoreText.text = "HighScore: " + _highscore.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _zigzagText = GameObject.Find("ZigZagText").GetComponent<TextMeshProUGUI>();
        _tapToStartText = GameObject.Find("TapToStartText").GetComponent<TextMeshProUGUI>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("Player.cs()::SpawnManager Commponent Not Found");
        }
        if (_player == null) {
            Debug.LogError("GameManager.cs::Start() - Player Component Not Found");
        }
        if (_zigzagText == null) {
            Debug.LogError("GameManager.cs::Start() - ZigZag Text Component Not Found");
        }
        if (_tapToStartText == null) {
            Debug.LogError("GameManager.cs::Start() - TapToStart Text Component Not Found");
        }     
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStarted == false && Input.GetMouseButtonDown(0)) {
            _gameStarted = true;
            StartGame();
            _player.StartGame();
        }

        if (_gameOver == true && Input.GetMouseButtonDown(0)) {
            _gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

    void StartGame() {
        _zigzagText.enabled = false;
        _tapToStartText.enabled = false;
        _spawnManager.StartGame();
    }

    public void AddScore() {
        _score++;
        _scoreText.text = "Score: " + _score.ToString();
        if (_score >= _highscore) {
            _highscore = _score;
        }
    }

    public void GameOver() {
        _gameOver = true;
        _zigzagText.enabled=true;
        _tapToStartText.enabled=true;
        _tapToStartText.text = "Tap To Restart Game";
        _spawnManager.GameOver();
        PlayerPrefs.SetInt("highscore", _highscore);
    }
}
