using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField]
    private float _asteroidMinSpeed = 5.0f;
    [SerializeField]
    private float _asteroidMaxSpeed = 10.0f;
    private float _asteroidSpeed;
    private Vector3 _asteroidDirection = new(-1, 0, 1);
    private float _xAngle, _yAngle, _zAngle;
    private float _rotationalSpeed;
    [SerializeField]
    private float _rotationalMinSpeed = 5.0f;
    [SerializeField]
    private float _rotationalMaxSpeed = 10.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _asteroidSpeed = Random.Range(_asteroidMinSpeed, _asteroidMaxSpeed);
        
        _xAngle = Random.Range(0, 360);
        _yAngle = Random.Range(0, 360);
        _zAngle = Random.Range(0, 360);

        transform.Rotate(_xAngle, _yAngle, _zAngle);

        _rotationalSpeed = Random.Range(_rotationalMinSpeed, _rotationalMaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_asteroidDirection * _asteroidSpeed * Time.deltaTime, Space.World);

        transform.Rotate(_rotationalSpeed * Time.deltaTime * Vector3.up, Space.Self);

        if (transform.position.x < -100.0f) {
            Destroy(this.gameObject);
        }
    }
}
