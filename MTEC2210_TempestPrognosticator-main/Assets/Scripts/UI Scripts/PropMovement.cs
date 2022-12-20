using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : MonoBehaviour
{

    private Vector3 start;
    private Vector3 end;
    public float movementSpeed;
    private bool forward;
    

    // Start is called before the first frame update
    void Start()
    {
        // sets the start and end positions based on child objects
        start = transform.Find("Start").position;
        end = transform.Find("End").position;
        forward = true;
 
        
    }

    // Update is called once per frame
    void Update()
    {
        // moves the object between the start and end points at a set speed
        // reverses direction when the endpoints are reached
        float step = Util.FrameDependant(movementSpeed);
        if (forward)
        {
            if (transform.position != end)
            {
                transform.position = Vector3.MoveTowards(transform.position, end, step);
                
            }
            else
            {
                forward = false;
            }
        }
        else 
        {
            if (transform.position != start)
            {
                transform.position = Vector3.MoveTowards(transform.position, start, step);
            }
            else
            {
                forward = true;
            }
        }
        
    }
}
