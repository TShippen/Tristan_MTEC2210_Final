using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;
    public GameManager gameManager;
    public Transform playerTransform; 
    private float cameraZOffset = -100;
    private float smoothTime;
    private Vector3 velocity = Vector3.zero;

    private float xMin, xMax, yMin, yMax;
    private float camX, camY;
    private float camOrthsize;
    private float cameraRatio;

    // camera tilt variables 
    public bool tiltingLeft;
    public float currentTilt;
    public float maxTilt;
    public float minTilt;
    public float tiltSpeed;


    void Start()
    {
        mainCamera = GetComponent<Camera>();

        smoothTime = 0.1f;
        tiltingLeft = false;
        currentTilt = 0;
        maxTilt = 2f;
        minTilt = -2f;
        tiltSpeed = 2;

        
    }


    void FixedUpdate() 
    {
        
        CameraFollow();
        TiltCamera();
    }

    void CameraFollow()
    {

        // set clamp variables
        xMin = gameManager.GetLevelSprite().bounds.min.x;
        xMax = gameManager.GetLevelSprite().bounds.max.x;
        yMin = gameManager.GetLevelSprite().bounds.min.y + .25f;
        yMax = gameManager.GetLevelSprite().bounds.max.y;
        camOrthsize = mainCamera.orthographicSize;
        cameraRatio = mainCamera.aspect * mainCamera.orthographicSize;

        camY = Mathf.Clamp(playerTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(playerTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);

    
        // clamps camera position based on sprite size from level sprite
        Vector3 targetPosition = new Vector3(camX, camY, cameraZOffset);

        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        
        
    }

    
    void TiltCamera()
    {
        
        if(tiltingLeft)
        {
            currentTilt += Util.FrameDependant(tiltSpeed);
            if(currentTilt > maxTilt)
            {
                tiltingLeft = false;
                currentTilt = maxTilt;
            }
        }
        else 
        {
            currentTilt -= Util.FrameDependant(tiltSpeed); 
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
