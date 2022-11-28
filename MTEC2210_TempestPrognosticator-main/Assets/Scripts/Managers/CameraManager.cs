using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private Transform playerTransform; 
    private float cameraZOffset = -100;
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

        // clamnps camera position based on sprite size from level sprite
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(playerTransform.position.x, -gameManager.GetLevelSprite().bounds.extents.x, gameManager.GetLevelSprite().bounds.extents.x),
            Mathf.Clamp(playerTransform.position.y, -gameManager.GetLevelSprite().bounds.extents.y, gameManager.GetLevelSprite().bounds.extents.y),
            cameraZOffset
        );


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
