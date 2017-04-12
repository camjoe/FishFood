using UnityEngine;
using System.Collections;

public class WaterCurrent: MonoBehaviour
{

    private Transform pSpot;
    public float speed = 4.0f;
    private bool inWater = true;

    // Use this for initialization
    void Start()
    {
        pSpot = GameObject.FindGameObjectWithTag("CurrentDest").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pSpot.position - transform.position), speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "BoatCollider")
        {
            inWater = false;
        }
    }

}
