using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5f;
    public float TurnSpeed = 2f;
    public float GroundDistance = 0.2f;
    /*    public float JumpHeight = 2f;
        public float GroundDistance = 0.2f;
        public float DashDistance = 5f;*/
    public LayerMask Ground;


    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;

    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        _inputs = transform.forward;
        
      //  _inputs.z = Input.GetAxis("Vertical");
     //   if (_inputs != Vector3.zero)
      //      transform.forward = _inputs;

        if (Input.GetMouseButton(0))
        {
            
            _inputs+= transform.right*TurnSpeed * Input.GetAxis("Mouse X");
            /*            Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Moved)
                        {*/
         //   Debug.Log(Input.GetAxis("Mouse X"));

          //  myCharacterController.Move(Vector3.right * (touch.deltaPosition.x * turnSpeed) * Time.deltaTime);   //moves the player left and right


        }
        //  }

    }

    private void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
      if(  other.name.Equals("Turn Right"))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
}
