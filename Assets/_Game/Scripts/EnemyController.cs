using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float Speed = 5f, TurnSpeed = 5f;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private Rigidbody _body;

    private Vector3 _direction = Vector3.forward;
    // Start is called before the first frame update

    //Running

    //Attacking

    //States
    public bool hasWeapon;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
      //  agent = GetComponent<NavMeshAgent>();

        _body = GetComponent<Rigidbody>();


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWeapon) Running();
    }

    private void Running()
    {
      //  _direction += transform.right * TurnSpeed * Random.Range(0f,1f);    //random turning
    }

    private void FixedUpdate()
    {
        _body.MovePosition(_body.position + _direction * Speed * Time.fixedDeltaTime);
    }
}
