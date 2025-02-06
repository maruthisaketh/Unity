using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    //public or private references
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField]
    private float _speedOfPlayer = 5.0f;
    [SerializeField]
    private float _speedMultiplier = 1.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngineFire;

    [SerializeField]
    private GameObject _rightEngineFire;
    private float _fireRate = 0.4f;
    private float _nextFireTime = 0.0f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldPowerupActive = false;
    private int _score = 0;

    private Animator _animator;
    private AudioSource _audioSource;
    private AudioClip _laserAudio, _explosionAudio;
    private PlayerInput _playerInput;

    private InputAction _moveInput, _fireInput;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        _moveInput = _playerInput.actions["Move"];
        _fireInput = _playerInput.actions["Attack"];
        if (_playerInput == null) {
            Debug.LogError("Player.cs::Awake() - Player Input Not found.");
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _laserAudio = Resources.Load("Audio/laser_shot") as AudioClip;
        _explosionAudio = Resources.Load("Audio/explosion_sound") as AudioClip;

        if (_spawnManager == null) {
            Debug.LogError("Player::Start() - The Spawn Manager is Not found");
        }
        if (_uiManager == null) {
            Debug.LogError("Player::Start() - The UI Manager is Not found");
        }
        if (_animator == null) {
            Debug.LogError("Player::Start() - Animator for the Player is Not found");
        }
        if (_audioSource == null) {
            Debug.LogError("Player.cs::Start() - Audio source for Player is Not Found");
        }
        if (_laserAudio != null) {
            _audioSource.clip = _laserAudio;
        }
        else {
            Debug.LogError("Player.cs::Start() - Laser Audio Not Found");
        }
        if (_explosionAudio == null) {
            Debug.LogError("Player.cs::Start() - Explosion Sound Not Found.");
        }
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();
        Debug.Log("" + _fireInput.IsPressed());
        if (_fireInput.IsPressed() && Time.time > _nextFireTime) {
            ShootLaser();
        }
    }

    void ShootLaser() {
        Vector3 laserOffset = new(0, 1.0f, 0);
        _nextFireTime = Time.time + _fireRate;

        if (_isTripleShotActive) {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else {
            Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
        }
        _audioSource.Play();
    }

    void CalculateMovement() {
        //Get the value of the direction which the player wants to move
        //Debug.Log("" + _moveInput.ReadValue<Vector2>());

        //_horizontalInput = Input.GetAxis("Horizontal");
        //_verticalInput = Input.GetAxis("Vertical");

        _horizontalInput = _moveInput.ReadValue<Vector2>().x;
        _verticalInput = _moveInput.ReadValue<Vector2>().y;

        //get direction of movement
        Vector3 direction = new(_horizontalInput, _verticalInput, 0);

        if (_isSpeedPowerupActive) {
            transform.Translate(_speedOfPlayer * _speedMultiplier * Time.deltaTime * direction);
        }
        else {
            transform.Translate(_speedOfPlayer * Time.deltaTime * direction);
        }

        //Handle Animations
        HandleAnimations(direction);

        //Restraining Player movement in Vertical Direction
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.0f, 0), transform.position.z);

        //Restraining Player movement in horizontal Direction
        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    private void HandleAnimations(Vector3 direction) {
        if (direction.x < 0) {
            _animator.SetBool("OnLeft", true);
            _animator.SetBool("OnRight", false);
        }
        else if (direction.x > 0) {
            _animator.SetBool("OnRight", true);
            _animator.SetBool("OnLeft", false);
        }
        else {
            _animator.SetBool("OnRight", false);
            _animator.SetBool("OnLeft", false);
        }
    }

    public void Damage() {

        if (_isShieldPowerupActive) {
            _shieldVisualizer.SetActive(false);
            _isShieldPowerupActive = false;
            return;
        }

        _lives--;

        if (_lives == 2) {
            _leftEngineFire.SetActive(true);
        }
        if (_lives == 1) {
            _rightEngineFire.SetActive(true);
        }

        _uiManager.UpdateLivesImg(_lives);

        if (_lives < 1) {
            //Stop Spawning Enemies and Destroy Player.
            _spawnManager.OnPlayerDeath();
            _leftEngineFire.SetActive(false);
            _rightEngineFire.SetActive(false);
            _audioSource.clip = _explosionAudio;
            _audioSource.Play();
            Destroy(gameObject);
        }
    }

    public void ActivateTripleShot() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedPowerup() {
        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerupActive = false;
    }

    public void ActivateShieldPowerup() {
        _shieldVisualizer.SetActive(true);
        _isShieldPowerupActive = true;
    }

    public void AddScore(int points) {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
