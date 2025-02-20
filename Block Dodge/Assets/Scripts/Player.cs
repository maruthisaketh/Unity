using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rigidBody;
    private float _speedOfPlayer = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        if (_rigidBody == null) {
            Debug.LogError("Player.cs::Start() - Rigidbody 2D Component not Found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -2.3f)
        {
            transform.position = new Vector2(-2.3f, transform.position.y);
        }
        else if (transform.position.x > 2.3f)
        {
            transform.position = new Vector2(2.3f, transform.position.y);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (touchPosition.x < 0)
            {
                _rigidBody.AddForce(Vector2.left * _speedOfPlayer);
            }
            else
            {
                _rigidBody.AddForce(Vector2.right * _speedOfPlayer);
            }
        }
        else
        {
            _rigidBody.linearVelocity = Vector2.zero;
        }
    }
}
