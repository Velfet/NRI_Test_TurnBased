using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraOffsetX = 5f;
    public float cameraOffsetY = 3f;


    
    void Update()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        if (transform.position.x < playerTransform.position.x - cameraOffsetX)
        {
            transform.position = new Vector3(playerTransform.position.x - cameraOffsetX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > playerTransform.position.x + cameraOffsetX)
        {
            transform.position = new Vector3(playerTransform.position.x + cameraOffsetX, transform.position.y, transform.position.z);
        }


        if (transform.position.y < playerTransform.position.y - cameraOffsetY)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.position.y - cameraOffsetY, transform.position.z);
        }
        else if (transform.position.y > playerTransform.position.y + cameraOffsetY)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.position.y + cameraOffsetY, transform.position.z);
        }
    }

}
