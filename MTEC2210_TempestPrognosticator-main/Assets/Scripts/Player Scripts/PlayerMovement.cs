using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask wall;
    public LayerMask enemy;


    // charging and launching variables
    public float minLaunchPower;            // maximum launch power
    public float maxLaunchPower;            // minimuim launch power
    public bool isCharging;                 // is charging active
    private bool isChargingUp;               // is charging moving up towards its max, used in the charge bar
    [SerializeField] private float currentCharge;             // the force being applied to the player launch when the button is released
    private Vector3 launchDirection;        // direction that the player will be launched, also where the player is "looking"
    private Vector3 mousePosition;          // mouse position taken as long as mouse button is held 
    private bool inFlight = false;          // becomes true if the player is in flight
    public float chargeRate;                // the speed at which the charge increases and decreases
    public float launchCost;
    public bool groundCheck;

    // attack variables
    public float attackSpeed;
    public bool attacking;
    public float attackTime;
    public bool draining;
    private GameObject currentEnemy;

    // player component references
    private Animator playerAnimator;         // player animator reference
    private new Rigidbody2D rigidbody2D;        // player rigidbody reference
    private BoxCollider2D boxCollider2D;    // player box collider reference
    private SpriteRenderer spriteRenderer;  // player sprite renderer reference

    // get player scripts
    private PlayerHealthStamina PlayerHealthStamina;
    private PlayerManager PlayerManager;

    public AudioClip boing;

    // Start is called before the first frame update
    void Start()
    {
        // set values for charging and launching
        isCharging = false;
        minLaunchPower = 1f;
        maxLaunchPower = 20f;
        chargeRate = 10f;
        launchCost = 10;

        //set values for attacking
        attackSpeed = .1f;
        attackTime = 2;
        attacking = false;


        // get player components
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();

        // get player scripts
        PlayerHealthStamina =  GetComponent<PlayerHealthStamina>();
        PlayerManager =  GetComponent<PlayerManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        // set collider size to sprite size
        //boxCollider2D.size = spriteRenderer.sprite.bounds.size;
        // determine mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        DetermineLaunchDirection();

        
        if (!attacking)
        {
            DetermineSpriteDirection();
            if (Input.GetMouseButton(0) && groundCheck)
            {
                Charging();
            }

            if (Input.GetMouseButtonUp(0) && groundCheck)
            {
                LaunchPlayer();
            }

            if(Input.GetMouseButtonDown(0) && !groundCheck)
            {
                StartCoroutine(StartAttack());
            }

        }

       
        
        
    }

    
    void DetermineSpriteDirection()
    {
        if(launchDirection.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void DetermineLaunchDirection()
    {
        launchDirection = mousePosition - transform.position;
        launchDirection = launchDirection / launchDirection.magnitude;   
  
    }

    void Charging()
    {


        isCharging = true;
        playerAnimator.SetBool("charging", isCharging);

        if(isChargingUp)
        {
            currentCharge += Util.FrameDependant(chargeRate);
            if(currentCharge > maxLaunchPower)
            {
                isChargingUp = false;
                currentCharge = maxLaunchPower;
            }
        }
        else
        {
            currentCharge -= Util.FrameDependant(chargeRate);
            if(currentCharge < minLaunchPower)
            {
                isChargingUp = true;
                currentCharge = minLaunchPower;
            }
        }
    }


    void LaunchPlayer() 
    {

        GetComponent<AudioSource>().PlayOneShot(boing);
        PlayerHealthStamina.ReduceHealthStaminaLaunch(currentCharge, minLaunchPower, maxLaunchPower);
        isCharging = false;
        playerAnimator.SetBool("charging", isCharging);
        rigidbody2D.AddForce(launchDirection * currentCharge, ForceMode2D.Impulse);



        // reset current charge
        currentCharge = 0;
    }

    

    public float GetCurrentCharge()
    {
        return currentCharge;
    }

 

    private IEnumerator StartAttack()
    {
        
        // checks if an enemy is underneath the player
        // this is a bit of a magic number situation, based on testing in the commented-out OnDrawGizmos function
        // this also returns a RaycastHit2D object so that I can get information about whatever it hits
        
        RaycastHit2D enemyCheck = Physics2D.BoxCast(boxCollider2D.bounds.center - transform.up * .1f, new Vector3(1, .25f, 0), 0f, Vector2.down, 0.5f, enemy);
        
        // if the enemy is alive...
        if (enemyCheck.collider.gameObject.GetComponent<EnemyHealth>().alive == true)
        {
            // get information about the enemy
            currentEnemy = enemyCheck.transform.gameObject;
            float enemyMaxHealth = currentEnemy.GetComponent<EnemyHealth>().maxHealth;
            
            // player attack should hurt enemy by 1/3 of it's health
            float attackValue = enemyMaxHealth / 3;

            // deal damage to enemy
            currentEnemy.GetComponent<EnemyHealth>().ReduceHealthDamage(attackValue);
            
            // let the enemy know you're attacking, it's only polite
            currentEnemy.GetComponent<GroundEnemyMovement>().beingAttacked = true;
            // change attacking bool to true
            attacking = true;

            while (!draining)
            {   
                rigidbody2D.position = Vector3.MoveTowards(rigidbody2D.position, currentEnemy.transform.position, attackSpeed);
                yield return null;
            }

            StartCoroutine(PlayerDraining());
                   
        }
    }

    // player drains life from the enemy
    private IEnumerator PlayerDraining() 
    {
        float attackTimeRemaining = attackTime;
        Debug.Log(attackTimeRemaining);
        
        
        
            
        while (attackTimeRemaining > 0)
        {
        
            rigidbody2D.position = new Vector3(currentEnemy.transform.position.x + .1f, currentEnemy.transform.position.y + .01f, currentEnemy.transform.position.z + -1);
            attackTimeRemaining -= Util.FrameDependant(1);

            yield return null;
        }
        
        // gives the player back 1/10th of their health
        PlayerHealthStamina.IncreaseCurrentHealthStamina(PlayerHealthStamina.maxStaminaHealth/10);
        currentEnemy.GetComponent<GroundEnemyMovement>().beingAttacked = false;    
        attacking = false;
        draining = false;
        currentCharge = maxLaunchPower/2; 
        LaunchPlayer();
            
        
    }

    // deals damage to the player and bounces them up
    private void PlayerHit()
    {
       
        PlayerHealthStamina.ReduceHealthStaminaDamage(.5f);
        rigidbody2D.AddForce(Vector2.up * .05f, ForceMode2D.Impulse);
    }
  

    private void OnCollisionEnter2D(Collision2D other) {

        // collision detection for enemy, either continues with attack or hurts the player
        if ((other.gameObject.tag == "Enemy"))
        {
            if (attacking == true)
            {
                draining = true;
            }
            else if (other.gameObject.GetComponent<EnemyHealth>().alive == true)
            {
                PlayerHit();
            }
        }

    
        // groundcheck that accounts for walls and ground layers
        if ((other.gameObject.layer == 6) | other.gameObject.layer == 8)
        {
            groundCheck = true;
        }
        
    }

    
    private void OnCollisionExit2D(Collision2D other) {

        // groundcheck that accounts for walls and ground layers
        if ((other.gameObject.layer == 6) | other.gameObject.layer == 8)
        {
            groundCheck = false;
        }
    }
}

//     void OnDrawGizmos()
//     {
//         boxCollider2D = GetComponent<BoxCollider2D>();
//         Gizmos.color = Color.red;

        
//         //Draw a Ray forward from GameObject toward the maximum distance
//         Gizmos.DrawRay(boxCollider2D.bounds.center, -transform.up * .25f);
//         //Draw a cube at the maximum distance
//         Gizmos.DrawWireCube(boxCollider2D.bounds.center - transform.up * .1f, new Vector3(1, .25f, 0));
        
//     }
// }
