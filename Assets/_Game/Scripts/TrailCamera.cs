using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCamera : MonoBehaviour
{
    public Transform target;
    private float trailDistance = 5.0f;  //was public
    private float heightOffset = 3.0f;   //was public
    // Start is called before the first frame update
    void Start()
    {
        heightOffset = transform.position.y - target.position.y;    //initialized here, because it makes it easier to configure the camera in the editor
        trailDistance = target.position.z - transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.rotation.eulerAngles = new Vector3(0, 0, 0);
        // transform.position = new Vector3(transform.position.x, target.position.y + heightOffset, target.position.z - trailDistance);
        transform.position = new Vector3(transform.position.x, target.position.y + heightOffset, target.position.z);
        //   transform = target.forward;
     
        transform.position -= trailDistance * target.forward;
        transform.LookAt(target.transform);
      //  transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
    }
}
