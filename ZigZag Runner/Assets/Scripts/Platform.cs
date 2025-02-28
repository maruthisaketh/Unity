using UnityEngine;

public class Platform : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
