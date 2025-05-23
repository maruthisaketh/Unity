using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField]
    private float _rotationSpeed = 30.0f;

    [SerializeField]
    private float _asteroidSpeed = 3.0f;

    private Animator _asteroidAnimator;

    private Player _player;

    private AudioClip _explosionAudio;
    private AudioSource _audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _asteroidAnimator = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _explosionAudio = Resources.Load<AudioClip>("Audio/explosion_sound");

        if (_asteroidAnimator == null) {
            Debug.LogError("Asteroid.cs::Start() - Asteroid Animator component Not found");
        }

        if (_player == null) {
            Debug.LogError("Asteroid.cs::Start() - Player Component Not Found");
        }

        if (_explosionAudio == null) {
            Debug.LogError("Asteriod.cs::Start() - Explosion Sound Not Found");
        }

        if (_audioSource == null) {
            Debug.LogError("Asteroid.cs::Start() - Audio Source component for Asteroid Not Found");
        }
        else {
            _audioSource.clip = _explosionAudio;
        }

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(_asteroidSpeed * Time.deltaTime * Vector3.down, Space.World);
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.back, Space.Self);

        if (transform.position.y < -5.0f) {
            float randX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randX, 7.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            else {
                Debug.LogError("Asteroid.cs::OnTriggerEnter2D() - Player component Not Found.");
            }
            _asteroidAnimator.SetTrigger("OnAsteroidExplosion");
            _asteroidSpeed = 0f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);
        }
        if (other.gameObject.CompareTag("Laser")) {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null) {
                if (laser.IsEnemyLaser() == false) {
                    _player.AddScore(5);
                    Destroy(other.gameObject);
                    _asteroidAnimator.SetTrigger("OnAsteroidExplosion");
                    _asteroidSpeed = 0f;
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    _audioSource.Play();
                    Destroy(this.gameObject, 2.5f);
                }
            }
        }
    }
}
