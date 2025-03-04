using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] GameObject _particlePrefab; // Assign Particle Prafab in EditMode.
    private GameManager _gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_particlePrefab == null) {
            Debug.LogError("Diamond.cs::Start() - Particle Prefab Not Found");
        }
        if (_gameManager == null) {
            Debug.LogError("Diamond.cs::Start() - GameManager Component Not Found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 45 * Time.deltaTime, 0, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            GameObject particle = Instantiate(_particlePrefab, gameObject.transform.position, Quaternion.identity);
            _gameManager.AddScore();
            Destroy(gameObject);
            Destroy(particle, 1.2f);
        }
    }

}
