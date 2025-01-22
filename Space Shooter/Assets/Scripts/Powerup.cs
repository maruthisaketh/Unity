using System.Transactions;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speedOfPowerup = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speedOfPowerup * Time.deltaTime * Vector3.down);

        if(transform.position.y < -5.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null) {
                player.ActivatePowerup();
            }
            Destroy(this.gameObject);
        }
    }
}
