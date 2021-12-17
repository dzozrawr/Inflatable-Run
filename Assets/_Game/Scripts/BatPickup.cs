using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPickup : PickUppable
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void effect(Collider other)
    {
        other.GetComponent<PlayerController>().giveBat(); //give bat to the player or enemy
    }
}
