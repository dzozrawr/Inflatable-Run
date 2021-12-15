using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingEndless : MonoBehaviour
{
    private float swingSpeed = 45f;

    Vector3 pointOfRotation;

    public bool vertical=true, horizontal=false;
    // Start is called before the first frame update
    void Start()
    {
        pointOfRotation= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + transform.lossyScale.y*1.5f, gameObject.transform.position.z) ;
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical)
        {
            transform.RotateAround(pointOfRotation, Vector3.right, swingSpeed * Time.deltaTime);
            // Debug.Log(transform.eulerAngles.x);
            if (Mathf.Abs(transform.eulerAngles.x) > 45 && Mathf.Abs(transform.eulerAngles.x) < 180)
            {
                swingSpeed = -swingSpeed;
            }

            if (Mathf.Abs(transform.eulerAngles.x) < 325 && Mathf.Abs(transform.eulerAngles.x) > 180)
            {
                swingSpeed = -swingSpeed;
            }
        }

        if (horizontal)
        {
            transform.RotateAround(pointOfRotation, Vector3.forward, swingSpeed * Time.deltaTime);
            // Debug.Log(transform.eulerAngles.x);
            if (Mathf.Abs(transform.eulerAngles.z) > 45 && Mathf.Abs(transform.eulerAngles.z) < 180)
            {
                swingSpeed = -swingSpeed;
            }

            if (Mathf.Abs(transform.eulerAngles.z) < 325 && Mathf.Abs(transform.eulerAngles.z) > 180)
            {
                swingSpeed = -swingSpeed;
            }
        }

        /*        Vector3 currentAngle = new Vector3(
                       Mathf.LerpAngle(transform.rotation.eulerAngles.x, destination.x, Time.deltaTime * swingSpeed),
                       Mathf.LerpAngle(transform.rotation.eulerAngles.y, 180, Time.deltaTime * swingSpeed),
                       Mathf.LerpAngle(transform.rotation.eulerAngles.z, destination.z, Time.deltaTime * swingSpeed)
                   );*/
        //  transform.eulerAngles = currentAngle;
    }
}
