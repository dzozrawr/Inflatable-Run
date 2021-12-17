using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : PlayerController
{
    // public float Speed = 5f, TurnSpeed = 5f;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;




    // private Rigidbody _body;

    private Vector3 _direction = Vector3.forward;
    // Start is called before the first frame update

    //Running

    //Attacking

    //States
    public bool wayPointsSet = false;

    private GameObject wayPoint;
    private Vector3 oldForward;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        //  agent = GetComponent<NavMeshAgent>();

        _body = GetComponent<Rigidbody>();
        //    _body.centerOfMass = new Vector3(0, 0, 0);
    }

    void Start()
    {
        defaultSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = transform.forward;
        if (!hasWeapon && !wayPointsSet) freeRunning();
        if (!hasWeapon && wayPointsSet) waypointFollow();
        if (hasWeapon && !wayPointsSet) attackPlayer();
    }

    private void freeRunning()
    {
        //transform.LookAt(player);
        //  _direction += transform.right * TurnSpeed * Random.Range(0f,1f);    //random turning
    }

    private void waypointFollow()
    {
        transform.LookAt(wayPoint.transform);

        if (Vector3.Distance(transform.position, wayPoint.transform.position) < 1f)
        {
            wayPointsSet = false;
            transform.forward = oldForward;
        }
    }

    private void attackPlayer()
    {
        transform.LookAt(player);

/*        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            player.GetComponent<PlayerController>().knockDown();
            transform.forward = oldForward;
            hasWeapon = false;
        }*/

    }

    private void FixedUpdate()
    {
        _body.MovePosition(_body.position + _direction * Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.collider.CompareTag("Player") && hasWeapon) //if im the player i can hit the enemy
        {
            //lookat player
            PlayerController pc = collision.collider.gameObject.GetComponent<PlayerController>();
            hitTheOtherGuy(pc);
            transform.forward = oldForward;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("WayPointTrigger")&& !wayPointsSet)
        {
            WaypointTrigger w = other.GetComponent<WaypointTrigger>();
            wayPoint = w.waypoints[Random.Range(0, w.waypoints.Length)];
            oldForward = transform.forward;
            wayPointsSet = true;
        }
    }
}
