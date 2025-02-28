using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedOfPlayer = 5.0f;
    private Rigidbody _rigidbody;
    private bool _gameStarted = false;
    private bool _gameOver = false;
    private Camera _camera;
    private SpawnManager _spawnManager;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null) {
            Debug.LogError("Player.cs::Awake() - RigidBody Component Not Found.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        if (_camera == null) {
            Debug.LogError("Player.cs::Start() - Main Camera Object Not Found");
        }

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.LogError("Player.cs()::SpawnManager Commponent Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStarted == false && Input.GetMouseButtonDown(0)) {
            _rigidbody.linearVelocity = new(0, 0, _speedOfPlayer);
            _gameStarted = true;
        }

        if(!Physics.Raycast(transform.position, Vector3.down, 1.2f)) {
            _gameOver = true;
            _camera.GetComponent<CameraSmoothFollow>().GameOver();
            _spawnManager.GameOver();
            _rigidbody.linearVelocity = Physics.gravity;
        }

        if(Input.GetMouseButtonDown(0) && _gameOver == false) {
            SwitchDirection();
        }
    }

    void SwitchDirection() {
        if (_rigidbody.linearVelocity.z > 0) {
            _rigidbody.linearVelocity = new(_speedOfPlayer, 0, 0);
        } else if (_rigidbody.linearVelocity.x > 0) {
            _rigidbody.linearVelocity = new(0, 0, _speedOfPlayer);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
