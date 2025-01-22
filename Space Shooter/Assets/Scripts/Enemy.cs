using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _speedOfEnemy = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(_speedOfEnemy * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5.0f) {
            float randX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randX, 7.0f, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser")) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
