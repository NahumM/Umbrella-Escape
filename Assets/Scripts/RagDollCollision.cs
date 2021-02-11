using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollCollision : MonoBehaviour
{
    [SerializeField] PlayerController pController;
    private void OnCollisionEnter(Collision collision)
    {
        if (pController != null)
            pController.BodyPartCollision(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pController != null)
            pController.BodyPartTrigger(other);
    }
}
