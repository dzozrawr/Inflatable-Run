﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPickup : PickUppable
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
        other.GetComponent<PlayerController>().explode();
    }
}
