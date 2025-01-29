using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 30.0f;

    [SerializeField]
    private float _asteroidSpeed = 3.0f;

    private Animator _asteroidAnimator;

    private Player _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _asteroidAnimator = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_asteroidAnimator == null) {
            Debug.LogError("Asteroid.cs::Start() - Asteroid Animator component Not found");
        }

        if(_player == null) {
            Debug.LogError("Asteroid.cs::Start() - Player Component Not Found");
        }

    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(this.gameObject, 2.5f);
        }
        if (other.gameObject.CompareTag("Laser")) {
            _player.AddScore(5);
            Destroy(other.gameObject);
            _asteroidAnimator.SetTrigger("OnAsteroidExplosion");
            _asteroidSpeed = 0f;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject,2.5f);
        }
    }
}
