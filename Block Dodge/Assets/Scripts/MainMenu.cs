using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private int _highscore;
    private TextMeshProUGUI _highscoreText;
    private bool _startGame = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _highscoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        if (_highscoreText == null) {
            Debug.LogError("MainMenu.cs::Start() - HighScore Text Component Not Found.");
        }
        _highscore = PlayerPrefs.GetInt("highscore", 0);
        _highscoreText.text = "High Score: " + _highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _startGame) { 
            SceneManager.LoadScene("Game");
        } else {
            _startGame = true;
        }
    }
}
