using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _speedOfEnemy = 4.0f;
    private Player _player;
    private Animator _enemyAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnimator = gameObject.GetComponent<Animator>();

        if (_player == null) {
            Debug.LogError("Enemy.cs::Start() - Player Game Object not Found");
        }

        if (_enemyAnimator == null) {
            Debug.LogError("Enemy.cs::Start() - Enemy Animator Component Not Found");
        }
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(_speedOfEnemy * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5.0f) {
            float randX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randX, 7.0f, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speedOfEnemy = 0f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2.5f);
        }
        else if (other.CompareTag("Laser")) {
            if (_player != null) {
                _player.AddScore(10);
            }
            Destroy(other.gameObject);
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speedOfEnemy = 0f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2.5f);
        }
    }
}
