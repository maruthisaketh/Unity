using UnityEngine;

public class Crate : MonoBehaviour
{
    private GameManager _gameManager;

    void Start() {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null) {
            Debug.LogError("Crate.cs::Start() - GameManager Not Found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -4.7f)
        {
            _gameManager.UpdateScore();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (_gameManager != null) {
                _gameManager.PlayerDead();
            } else {
                Debug.LogError("Crate.cs::OnCollisionEnter2D() - GameManager Component Not found.");
            }
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        } 
    }
}
