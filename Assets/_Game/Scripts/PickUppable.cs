using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUppable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            effect(other);
            //effect, could be made abstract in case of multiple power ups
            Destroy(gameObject);
        }
    }

    public abstract void effect(Collider other);
}
