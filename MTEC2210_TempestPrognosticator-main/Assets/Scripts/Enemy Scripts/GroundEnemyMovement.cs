using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public float speed;
    public float groundRaycastDistance;
    public float wallRaycastDistance;
    private Transform frontBumperTransform;
    
    public bool beingAttacked;
    public bool movingRight;

    public LayerMask ground;
    public LayerMask wall;

    public AudioClip squeak;

    // Start is called before the first frame update
    void Start()
    {
        // set speed randomly between .5 and 2
        float speedRandomizer = Random.value;
        speed = Util.RemapRange(speedRandomizer, 0, 1, .5f, 2);

        // set movement variables
        groundRaycastDistance = .2f;
        wallRaycastDistance = .1f;
        movingRight = true;
        frontBumperTransform = transform.Find("FrontBumper").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() 
    {
        Patrol();

        // debug tools used to visualize the raycast
        // Debug.DrawRay(frontBumperTransform.position, new Vector2(0, -groundRaycastDistance), Color.green);
        // Debug.DrawRay(frontBumperTransform.position, new Vector2(wallRaycastDistance, 0), Color.green);
    }

    
    private void Patrol()
    {
        

        // check if being attacked and if alive, set movement speed accordingly
        float movementSpeed;
        if (beingAttacked == true || enemyHealth.alive == false)
        {
            movementSpeed = 0;
            
        }
        else
        {
            movementSpeed = speed;
        }
        
        // move object "forward"
        transform.Translate(Vector2.right * Util.FrameDependant(movementSpeed));

        // use raycasts to check for walls and ground
        bool groundInfo = Physics2D.Raycast(frontBumperTransform.position, Vector2.down, groundRaycastDistance, ground);
        bool wallInfo =  Physics2D.Raycast(frontBumperTransform.position, new Vector2(wallRaycastDistance, 0), wallRaycastDistance, wall);
        
        

        // if wall is present or ground is not, flip the object depending on the direction it's already facing
        if (groundInfo == false || wallInfo == true)
        {
            
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                wallRaycastDistance *= -1;
                
            }
            else 
            {

                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
                wallRaycastDistance *= -1;
                
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        // if collide with player layer (layer 7) play squeak sound
        if (other.gameObject.layer == 7)
        {
            GetComponent<AudioSource>().PlayOneShot(squeak);
        }
    }




}
