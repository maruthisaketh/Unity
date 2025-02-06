using UnityEngine;

public class Laser : MonoBehaviour {
    //Speed variable
    [SerializeField]
    private float _speedOfLaser = 8.0f;
    [SerializeField]
    private bool _isEnemyLaser = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (_isEnemyLaser == true) {
            MoveLaserDown();
        }
        else {
            MoveLaserUp();
        }
    }
    void MoveLaserDown() {
        transform.Translate(_speedOfLaser * Time.deltaTime * Vector3.down);
        if (transform.position.y < -8.0f) {
            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
    void MoveLaserUp() {
        transform.Translate(_speedOfLaser * Time.deltaTime * Vector3.up);
        if (transform.position.y > 8.0f) {
            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void AssignEnemyLaser() {
        _isEnemyLaser = true;
    }

    public bool IsEnemyLaser() {
        return _isEnemyLaser;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (_isEnemyLaser && other.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            else {
                Debug.LogError("Laser.cs::OnTriggerEnter2D() - Player Component Not found");
            }
            Destroy(this.gameObject);
        }
    }
}
