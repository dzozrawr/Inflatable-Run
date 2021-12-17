using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public float TurnSpeed = 5f;
    public float GroundDistance = 0.2f;
    /*    public float JumpHeight = 2f;
        public float GroundDistance = 0.2f;
        public float DashDistance = 5f;*/
    public LayerMask Ground;


    protected Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    private bool once = false, turning;

    private GameObject turnEnd = null;
    private float turnDistance, turnAngle;
    private Vector3 initialDirection;
    private float knockedOutDuration=0.5f, invincibilityDuration=4f;

    protected bool isInvincible=false;    //ignores collision effects when invincible

    protected float defaultSpeed;

    public Animator playerAnimator;

    public CapsuleCollider standingCapsule, crawlingCapsule;

    protected bool hasWeapon = false;

    public float explosionRadius = 0F;
    public float explosionPower = 20.0F;


    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = Speed;

        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        //    _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        _inputs = transform.forward;

        //  _inputs.z = Input.GetAxis("Vertical");
        //   if (_inputs != Vector3.zero)
        //      transform.forward = _inputs;

        if (Input.GetMouseButton(0))
        {

            _inputs += transform.right * TurnSpeed * Input.GetAxis("Mouse X");
            /*            Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Moved)
                        {*/
            //   Debug.Log(Input.GetAxis("Mouse X"));

            //  myCharacterController.Move(Vector3.right * (touch.deltaPosition.x * turnSpeed) * Time.deltaTime);   //moves the player left and right


        }
        //  }
/*        if (turning)
        {
            float curDistance = 1f;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, initialDirection, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("TurnEnd"))
                {
                    curDistance = Vector3.Distance(transform.position, hit.point);
                }
            }


            float rotationProgress = (turnDistance - curDistance) / turnDistance;

            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, turnAngle* rotationProgress, transform.rotation.eulerAngles.z));
        }*/
    }



    private void FixedUpdate()
    {
         _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
      //  _body.AddForce(_inputs * Speed *100f* Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle")&& !isInvincible)
        {
            knockDown();
        }

        if (transform.gameObject.CompareTag("Player")&&collision.collider.CompareTag("Enemy") && hasWeapon) //if im the player i can hit the enemy
        {
            //lookat player
            PlayerController pc = collision.collider.gameObject.GetComponent<PlayerController>();
            hitTheOtherGuy(pc);

        }
    }

    protected void hitTheOtherGuy(PlayerController pc)
    {
        playerAnimator.SetTrigger("Hit");
        pc.knockDown();
        hasWeapon = false;
    }

    public void explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb!=_body)
            {
                rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 0.0F, ForceMode.Impulse);
                hit.GetComponent<PlayerController>().knockDown();
                
            }
               
        }
       
    }



    public void knockDown()
    {
        //stop //play animation 
        isInvincible = true;
        Speed = 0f;
        playerAnimator.SetTrigger("KnockDown");

        //time delay
        
       // playerAnimator.speed = 0f;
       // Invoke(nameof(getUpFromKnockDown), knockedOutDuration);
        //continue //invincibility
    }

    

    public void getUpFromKnockDown()
    {
        //set invincibility flag

        //playerAnimator.speed = 1f;
        Speed = defaultSpeed;
        Invoke(nameof(disableInvincibility), invincibilityDuration);
        
        //invoke disable method
    }

    protected void disableInvincibility()
    {
        isInvincible = false;
    }

    protected void transitionToCrawl()
    {
       
        playerAnimator.SetTrigger("Crawl"); //play animation
        crawlingCapsule.enabled = true;
        standingCapsule.enabled = false;
    }

    protected void transitionToRunning()
    {
        playerAnimator.SetTrigger("Run"); //play animation
        standingCapsule.enabled = true;
        crawlingCapsule.enabled = false;
    }

    public void setSpeed(float s)
    {
        Speed = s;
    }

    public void setDefaultSpeed()
    {
        Speed = defaultSpeed;
    }

    public void giveBat()
    {
        hasWeapon = true;
       // Debug.Log("hasWeapon = true;");
        //enable the bat model
    }
    protected void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("RotateTrigger"))
        {        
            transform.forward = other.transform.forward;
        }

        if (other.CompareTag("CrawlTriggerOff"))
        {
            Debug.Log("CrawlTriggerOff");
            transitionToRunning();
        }

        if (other.CompareTag("CrawlTrigger"))
        {
            Debug.Log("CrawlTrigger");
            transitionToCrawl();
        }

        if (other.CompareTag("JumpBox"))
        {
            playerAnimator.SetTrigger("Jump");
        }


        if (other.name.Equals("TurnStart"))
        {
            if (!once)
            {
                if (other.transform.parent.GetChild(1).CompareTag("TurnEnd"))
                {
                    turnEnd = other.transform.parent.GetChild(1).gameObject;

                    RaycastHit hit;
                    initialDirection = transform.TransformDirection(-transform.right);
                    if (Physics.Raycast(transform.position, transform.TransformDirection(-transform.right), out hit, Mathf.Infinity))
                    {
                        if (hit.collider.CompareTag("TurnEnd"))
                        {
                            turnDistance = Vector3.Distance(transform.position, hit.point);
                          //  Debug.Log("turnDistance = Vector3.Distance(transform.position, hit.point);");
                        }
                    }

                  //  Debug.Log("turnDistance=" + turnDistance);

                   // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                  
                    //Physics.Raycast(transform.position, transform.TransformDirection(-transform.right), out hit, Mathf.Infinity)
                    //  Vector3 a = turnEnd.transform.position - transform.position;
                    turnAngle = Vector3.SignedAngle(transform.forward, -transform.right,transform.up);
                   // Debug.Log("turnAngle=" + turnAngle);
                    turning = true;
                    // transform.forward = -transform.right;
                    ///  transform.LookAt(turnEnd.transform);
                    once = true;
                }
            }

        }
    }
}
