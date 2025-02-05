using UnityEngine;

public class Powerup : MonoBehaviour {
    [SerializeField]
    private float _speedOfPowerup = 3.0f;

    /*
    ID = PowerUp
    0 = Triple Shot
    1 = Speed
    2 = Shield
    */
    [SerializeField]
    private int _powerupID = 0;

    private AudioClip _powerupClip;

    [SerializeField]
    private float _volume = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _powerupClip = Resources.Load<AudioClip>("Audio/power_up_sound");
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(_speedOfPowerup * Time.deltaTime * Vector3.down);

        if (transform.position.y < -5.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_powerupClip, transform.position, _volume);
            if (player != null) {
                switch (_powerupID) {
                    case 0: player.ActivateTripleShot(); break;
                    case 1: player.ActivateSpeedPowerup(); break;
                    case 2: player.ActivateShieldPowerup(); break;
                    default: Debug.Log("Failed to Collect Power UP"); break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
