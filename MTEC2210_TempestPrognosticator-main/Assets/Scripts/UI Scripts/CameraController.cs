using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; 
    private Vector3 cameraOffset = new Vector3(0, 0, -10);
    private float smoothTime;
    private Vector3 velocity = Vector3.zero;

    public bool tiltingLeft;
    public float currentTilt;
    public float maxTilt;
    public float minTilt;
    public float tiltSpeed;


    void Start()
    {
        smoothTime = 0.25f;
        tiltingLeft = false;
        currentTilt = 0;
        maxTilt = 2f;
        minTilt = -2f;
        tiltSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
        CameraFollow();
        TiltCamera();

    }

    void CameraFollow()
    {
        Vector3 targetPosition = playerTransform.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);  
    }

    
    void TiltCamera()
    {
        
        if(tiltingLeft)
        {
            currentTilt += Time.deltaTime * tiltSpeed;
            if(currentTilt > maxTilt)
            {
                tiltingLeft = false;
                currentTilt = maxTilt;
            }
        }
        else 
        {
            currentTilt -= Time.deltaTime * tiltSpeed; 
            if(currentTilt < minTilt)
            {
                tiltingLeft = true;
                currentTilt = minTilt;
            }
        }

        

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            Mathf.Clamp(currentTilt, minTilt - 1, maxTilt + 1)
        );
        

    }

}
