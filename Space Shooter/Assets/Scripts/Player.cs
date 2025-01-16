using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private references
    private float _horizontalInput;
    private float _verticalInput;
    private readonly float _speedOfPlayer = 3.2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Take the current position and Assign a start position(0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        //Get the value of the direction which the player wants to move
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        //get direction of movement
        Vector3 direction = new(_horizontalInput, _verticalInput, 0);
        transform.Translate(_speedOfPlayer * Time.deltaTime * direction);

        //Restraining the Player movement in Vertical Direction
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y < -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }

        //Restraining Player movement in horizontal Direction
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }
}
