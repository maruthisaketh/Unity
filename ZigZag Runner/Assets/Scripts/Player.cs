using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speedOfPlayer = 4.5f;

    private Rigidbody _rigidbody;
    private bool _gameStarted = false;
    private bool _gameOver = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
