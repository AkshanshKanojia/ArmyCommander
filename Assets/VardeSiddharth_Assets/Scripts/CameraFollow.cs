using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Vector3 cameraRotation = new Vector3(30, 0, 0);
    [SerializeField]
    Vector3 cameraOffsetFromPlayer = new Vector3(0, 5, -7);
    [SerializeField]
    Transform playerTransformToFollow;

    [SerializeField]
    float cameraFollowSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(cameraRotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            playerTransformToFollow.position + cameraOffsetFromPlayer, cameraFollowSpeed * Time.deltaTime);

        //transform.position = Vector3.Lerp(transform.position,
        //    playerTransformToFollow.position + cameraOffsetFromPlayer, cameraFollowSpeed * Time.deltaTime);
    }
}
