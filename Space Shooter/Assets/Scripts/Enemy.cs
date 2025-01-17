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

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Hit: " + other.gameObject.name);
    }
}
