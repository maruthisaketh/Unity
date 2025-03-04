using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedOfPlayer = 10.0f;
    private Rigidbody _rigidbody;
    private Camera _camera;
    private GameManager _gameManager;

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

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null) {
            Debug.LogError("Player.cs()::GameManager Commponent Not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!Physics.Raycast(transform.position, Vector3.down, 1.2f)) {
            _camera.GetComponent<CameraSmoothFollow>().GameOver();
            _gameManager.GameOver();
            _rigidbody.linearVelocity = Physics.gravity;
        }

        if(Input.GetMouseButtonDown(0)) {
            SwitchDirection();
        }
    }

    public void SwitchDirection() {
        if (_rigidbody.linearVelocity.z > 0) {
            _rigidbody.linearVelocity = new(_speedOfPlayer, 0, 0);
        } else if (_rigidbody.linearVelocity.x > 0) {
            _rigidbody.linearVelocity = new(0, 0, _speedOfPlayer);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public void StartGame() {
        _rigidbody.linearVelocity = new(0, 0, _speedOfPlayer);
    }
}
