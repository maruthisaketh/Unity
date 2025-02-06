using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _speedOfEnemy = 3.5f;
    private Player _player;

    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;


    private Animator _enemyAnimator;
    private AudioSource _audioSource;
    private AudioClip _explosionAudio;
    private bool _isEnemyAlive = true;

    private Vector3 _laserOffset = new Vector3(0, 0.1f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnimator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _explosionAudio = Resources.Load<AudioClip>("Audio/explosion_sound");

        if (_player == null) {
            Debug.LogError("Enemy.cs::Start() - Player Game Object not Found");
        }

        if (_enemyAnimator == null) {
            Debug.LogError("Enemy.cs::Start() - Enemy Animator Component Not Found");
        }

        if (_explosionAudio == null) {
            Debug.LogError("Enemy.cs::Start() - Explosion Sound Not Found");
        }

        if (_audioSource != null) {
            _audioSource.clip = _explosionAudio;
        }
        else {
            Debug.LogError("Enemy.cs::Start() - Audio Source for Enemy Not Found.");
        }
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();
        if (Time.time > _canFire && _isEnemyAlive) {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position + _laserOffset, Quaternion.identity) as GameObject;
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++) {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void CalculateMovement() {
        transform.Translate(_speedOfEnemy * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5.0f) {
            float randX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randX, 7.0f, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        _isEnemyAlive = false;
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speedOfEnemy = 0f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            _audioSource.Play();
            Destroy(gameObject, 2.5f);
        }
        else if (other.CompareTag("Laser")) {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null) {
                if (laser.IsEnemyLaser() == false) {
                    if (_player != null) {
                        _player.AddScore(10);
                    }
                    Destroy(other.gameObject);
                    _enemyAnimator.SetTrigger("OnEnemyDeath");
                    _speedOfEnemy = 0f;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    _audioSource.Play();
                    Destroy(gameObject, 2.5f);
                }
            }
        }
    }
}
