using UnityEngine;

public class Platform : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 2.0f);
        }
    }
}
