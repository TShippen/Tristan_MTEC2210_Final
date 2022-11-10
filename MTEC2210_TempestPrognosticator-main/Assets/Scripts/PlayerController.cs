using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // public or serialized variables
    public GameManager gameManager;
    public float moveSpeed;
    public float launchPower;
    public float maxCharge;

    

    // private variables
    private Rigidbody2D rigidbody2D;
    private float startChargeTime;

    private bool inFlight = false;






    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        maxCharge = 3f;


    }

    // Update is called once per frame
    void Update()
    {
        // player horizonal movement
        float xMove =  Input.GetAxisRaw("Horizontal");
        transform.Translate(xMove * moveSpeed * Time.deltaTime, 0, 0);

        // player launch movement
        if (Input.GetMouseButtonDown(0))
        {
            startChargeTime = Time.time;          
        }

        // player initiate launch
        if (Input.GetMouseButtonUp(0))
         {
            float totalChargeTime = Time.time - startChargeTime;
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePosition - transform.position).normalized;

            rigidbody2D.velocity = (new Vector2(direction.x, direction.y) * LaunchForce(totalChargeTime, maxCharge, launchPower));

         }
    }


    void FixedUpdate() 
    {
        
    }

    private float LaunchForce(float chargeTime, float maxChargeTime, float launchForce) 
    {
        float force = (Mathf.Clamp01(chargeTime / maxChargeTime) * launchForce);

        return force;
    }
}
