using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {
    public enum triggerType { buttonPress, cameraView, collision, noAction};
    public enum button { trigger, any };

    public triggerType textActionTrigger;
    public button activator;

    public string lookTowardsName;
    public Transform lookDirection;

    public bool hasCollided = false;


    private void Start()
    {
        if (textActionTrigger == triggerType.cameraView)
        {
            lookDirection = GameObject.Find(lookTowardsName).transform;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("got collisions");
        if (textActionTrigger == triggerType.collision)
        {
            Debug.Log("collision type text");

            if (collision.collider.tag == "Food")
            {
                Debug.Log("it is food");

                hasCollided = true;
            }
        }
    }
}
