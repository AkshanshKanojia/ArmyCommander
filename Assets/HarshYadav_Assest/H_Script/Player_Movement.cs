using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    Rigidbody playerRb;
    [SerializeField]
    float speed = 5;
    float horizontalInput, verticalInput;
    Vector3 movement;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        movement = new Vector3(horizontalInput, 0, verticalInput).normalized * Time.deltaTime * speed;
        transform.position += movement;
        
    }
}
