using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRatPatrol : MonoBehaviour
{
    public float speed;
    public float raycastDistance;
    public Rigidbody2D rigidbody2D;
    private BoxCollider2D frontBumperCollider;
    private Transform frontBumperTransform;
    
    public bool movingRight;

    public LayerMask ground;
    public LayerMask wall;



    // Start is called before the first frame update
    void Start()
    {
        speed = 20;
        raycastDistance = .5f;
        movingRight = true;
        frontBumperTransform = transform.Find("FrontBumper").GetComponent<Transform>();
        frontBumperCollider = transform.Find("FrontBumper").GetComponent<BoxCollider2D>();
    }


    void Update() 
    {
       Debug.DrawRay(frontBumperTransform.position, new Vector2(0, -raycastDistance), Color.green);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);

        bool groundInfo = Physics2D.Raycast(frontBumperTransform.position, Vector2.down, raycastDistance, ground);
        if (groundInfo == false)
        {
            
            if (movingRight == true)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                movingRight = false;
                speed *= -1;
            }
            else 
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
                movingRight = true;
                
            }
        }
        
    }

    

   


}
