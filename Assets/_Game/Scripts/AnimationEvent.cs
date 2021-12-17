using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private PlayerController playerC;
    // Start is called before the first frame update
    void Start()
    {
        playerC = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getUpFromKnockDown()
    {
        playerC.getUpFromKnockDown();
    }
}
