using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "BoatCollider")
        {

        }
    }
}
