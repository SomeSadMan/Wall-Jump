using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera cameraTransform;
    private Vector3 cameraPos;
    static public bool playerIsAlive;
    private bool reachedCameraObservePoint;
    
    

    private float previousPlayerPositionY;

    void Start()
    {
        playerIsAlive = true;
        cameraPos = transform.position;
        previousPlayerPositionY = playerTransform.position.y;
    }


    void Update()
    {
        if (playerIsAlive)
        {
            PlayerObserving();
        }
        else
        {
            ResetCamera();
        }
        
  
       
  
    }

    private void PlayerObserving()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = playerTransform.position.x;

        if (playerTransform.position.y > previousPlayerPositionY && reachedCameraObservePoint)
        {
            newPosition.y = playerTransform.position.y;


        }

        transform.position = newPosition;
        previousPlayerPositionY = playerTransform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(reachedCameraObservePoint);
            reachedCameraObservePoint = true;
        }
       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(reachedCameraObservePoint);
            reachedCameraObservePoint = false;
        }
    }

    private void ResetCamera()
    {
        transform.position = cameraPos;
        playerIsAlive = true;
    }

}
