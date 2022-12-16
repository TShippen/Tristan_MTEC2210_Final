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
        start = transform.Find("Start").position;
        end = transform.Find("End").position;
        forward = true;
 
        
    }

    // Update is called once per frame
    void Update()
    {
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
