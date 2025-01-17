using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour {
    //Speed variable
    private float _speedOfLaser = 8.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Translate(_speedOfLaser * Time.deltaTime * Vector3.up);
        if (transform.position.y > 8.0f) {
            Destroy(gameObject);
        }
    }
}
