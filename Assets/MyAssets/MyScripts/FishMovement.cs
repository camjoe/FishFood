using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour {
    public float speed = 1.5f;
    public float rotateSpeed = 1f;
    public float jumpForce = 1f;
    public Transform startJumpRotation;
    public Transform endJumpRotation;
    public GameObject prevTarget;
    public GameObject nextTarget;

    private bool isJumping;
    Vector3 targetRelPos;
    private bool isEating;
    private Rigidbody rb;
    private int prevJump;
    private int nextJump;

    private void Start()
    {
        isEating = false;
        isJumping = false;
        prevJump = 0;
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        int time = (int)Time.fixedTime;
        if (time - prevJump >= nextJump)
        {
            prevJump = time;
            JumpStart();
            nextJump = Random.Range(8, 16);


        }
        else if (time - prevJump >= 1 && isJumping)
        {
            JumpEnd();
        }
    }

    void Update()
    {
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");

        if (isEating)
        {
            targetRelPos = SwimTowardsFood(food);
        }
        else
        {
            targetRelPos = GeneralSwim();
        }

        SetMove(targetRelPos);
    }

    // Fish jumps out of water
    void JumpEnd()
    {
        isJumping = false;

        //rb.AddForce(transform.up * jumpForce);
        rb.velocity += new Vector3(0.0f, -1.0f * jumpForce, 0.0f);
        rb.rotation *= endJumpRotation.rotation;
    }

    // Fish jumps out of water
    void JumpStart()
    {
        isJumping = true;

        //rb.AddForce(transform.up * jumpForce);
        rb.velocity += new Vector3(0.0f, jumpForce, 0.0f);
        rb.rotation *= startJumpRotation.rotation;
    }

    // Swim towards food once meal is found
    Vector3 SwimTowardsFood(GameObject[] food)
    {
        int meal = Random.Range(0, food.Length);


        if (food[meal] && transform.position != food[meal].transform.position)
        {
            Vector3 mealPosition = food[meal].transform.position;
            targetRelPos = mealPosition - transform.position;
        }
        else
        {
            isEating = false;
        }

        return targetRelPos;
    }

    // Swim randomly between two set locations
    Vector3 GeneralSwim()
    {
        //Debug.Log(Vector3.Distance(transform.position, nextTarget.transform.position) < 1.5f);
        if (Vector3.Distance(transform.position, nextTarget.transform.position) < 1.5f)
        {
            Debug.Log(nextTarget);
            GameObject temp = nextTarget;
            nextTarget = prevTarget;
            prevTarget = temp;
        }

        targetRelPos = nextTarget.transform.position - transform.position;

        return targetRelPos;
    }

    // Set Move in action
    void SetMove(Vector3 targetRelPos)
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetRelPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
    }


    void SetIsEating(bool isHungry)
    {
        isEating = isHungry;
    }
}
