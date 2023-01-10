using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator playerAnimator;
    float playerAnimationSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 playerInputDirection;

    [SerializeField]
    float playerMoveSpeed = 5;
    [SerializeField]
    float playerRotateSpeed = 1000;
    [SerializeField]
    Joystick joystickInput;
    [SerializeField]
    Transform movingInDirectionSpriteTransform;

    Vector3 movingInDirectionSpriteNewPosition;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        TakePlayerInput();

        MoveAndRotatePlayer();
    }

    void TakePlayerInput()
    {
        if (joystickInput == null)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalInput = joystickInput.Horizontal;
            verticalInput = joystickInput.Vertical;
        }

        SetThePlayerInputDirection();
    }

    void SetThePlayerInputDirection()
    {
        playerInputDirection = ((Vector3.forward * verticalInput)
            + (Vector3.right * horizontalInput)).normalized;

        if (playerAnimator != null)
        {
            SetPlayerAnimation();
        }
    }

    void MoveAndRotatePlayer()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            //SetMovingInDirectionSpritePosition();

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(playerInputDirection), playerRotateSpeed * Time.deltaTime);

            //transform.position += transform.forward * playerMoveSpeed * Time.deltaTime;

            transform.position += playerInputDirection * playerMoveSpeed * Time.deltaTime;
        }
        SetMovingInDirectionSpritePosition();
    }

    void SetPlayerAnimation()
    {
        playerAnimationSpeed = playerInputDirection.magnitude;

        playerAnimator.SetFloat("speed", playerAnimationSpeed);
    }

    void SetMovingInDirectionSpritePosition()
    {
        if (movingInDirectionSpriteTransform != null)
        {
            movingInDirectionSpriteNewPosition.x = transform.position.x + joystickInput.Direction.x;
            movingInDirectionSpriteNewPosition.y = transform.position.y;
            movingInDirectionSpriteNewPosition.z = transform.position.z + joystickInput.Direction.y;

            movingInDirectionSpriteTransform.position = movingInDirectionSpriteNewPosition;
        }
    }
}
