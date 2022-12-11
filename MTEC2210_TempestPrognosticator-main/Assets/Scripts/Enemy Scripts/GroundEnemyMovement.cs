using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    public EnemyHealth health;
    public float speed;
    public float groundRaycastDistance;
    public float wallRaycastDistance;
    private Transform frontBumperTransform;
    
    public bool beingAttacked;
    public bool movingRight;

    public LayerMask ground;
    public LayerMask wall;



    // Start is called before the first frame update
    void Start()
    {
        float speedRandomizer = Random.value;
        speed = Util.RemapRange(speedRandomizer, 0, 1, .5f, 2);
        groundRaycastDistance = .5f;
        wallRaycastDistance = 1f;
        movingRight = true;
        frontBumperTransform = transform.Find("FrontBumper").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() 
    {
        Patrol();
        Debug.DrawRay(frontBumperTransform.position, new Vector2(0, -groundRaycastDistance), Color.green);
    }

    
    private void Patrol()
    {
        

        // check if being attacked, set movement speed accordingly
        float movementSpeed;
        if (beingAttacked == true || health.alive == false)
        {
            movementSpeed = 0;
            
        }
        else
        {
            movementSpeed = speed;
        }
        

        bool groundInfo = Physics2D.Raycast(frontBumperTransform.position, Vector2.down, groundRaycastDistance, ground);
        bool wallInfo =  Physics2D.Raycast(frontBumperTransform.position, frontBumperTransform.forward, wallRaycastDistance, ground);
        
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        if (groundInfo == false || wallInfo == true)
        {
            
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                
            }
            else 
            {

                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
                
            }
        }

    }

    

    

   


}
