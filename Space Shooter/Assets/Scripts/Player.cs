using UnityEngine;

public class Player : MonoBehaviour {
    //public or private references
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField]
    private float _speedOfPlayer = 4.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.2f;
    private float _nextFireTime = 0.0f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.Log("The Spawn Manager is Null");
        }
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime) {
            ShootLaser();
        }
    }

    void ShootLaser() {
        Vector3 laserOffset = new(0, 0.8f, 0);
        _nextFireTime = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
    }

    void CalculateMovement() {
        //Get the value of the direction which the player wants to move
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        //get direction of movement
        Vector3 direction = new(_horizontalInput, _verticalInput, 0);
        transform.Translate(_speedOfPlayer * Time.deltaTime * direction);

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

    public void Damage() {
        _lives--;

        if (_lives < 1) {
            //Stop Spawning Enemies and Destroy Player.
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
