using UnityEngine;
using System.Collections;

public class FishMove : MonoBehaviour {
	public float speed=1.5f;
	public float rotateSpeed=1f;
    public float jumpForce = 1f;
    public Transform startJumpRotation;
    public Transform endJumpRotation;

    private bool isJumping;
    private GameObject[] targets;  
	Vector3 targetRelPos;
    private int prevTarget;
    private int nextTarget;
    private bool isEating;
    private Rigidbody rb;
    private int prevJump;



    private void Start()
    {
        isEating = false;
        isJumping = false;
        prevTarget = 0;
        nextTarget = 0;
        prevJump = 0;
        targets = GameObject.FindGameObjectsWithTag("WhaleDest");
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        /*int time = (int)Time.fixedTime;
        int nextJump = Random.Range(8, 16);
        if (time - prevJump >= nextJump)
        {
            prevJump = time;
            JumpStart();

        }
        else if(time - prevJump >= 1 && isJumping)
        {
            JumpEnd();
        }*/
    }

    void Update () {
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");


        targetRelPos = GeneralSwim();

        SetMove(targetRelPos);
    }

    // Fish jumps out of water
    void JumpEnd()
    {
        isJumping = false;

        Debug.Log("Jumping");
        //rb.AddForce(transform.up * jumpForce);
        rb.velocity += new Vector3(0.0f, -jumpForce, 0.0f);
        rb.rotation *= endJumpRotation.rotation;
    }

    // Fish jumps out of water
    void JumpStart()
    {
        isJumping = true;

        Debug.Log("Jumping");
        //rb.AddForce(transform.up * jumpForce);
        rb.velocity += new Vector3(0.0f, jumpForce, 0.0f);
        rb.rotation *= startJumpRotation.rotation;
    }

    // Swim towards food once meal is found
    Vector3 SwimTowardsFood(GameObject[] food)
    {
        int meal = Random.Range(0, targets.Length);


        if(food[meal] && transform.position != food[meal].transform.position)
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

    // Swim randomly between set locations
    Vector3 GeneralSwim()
    {
        if (targets.Length <= 0)
        {
            return transform.position;
        }

        if (Vector3.Distance(transform.position, targets[nextTarget].transform.position) < 2.0)
        {
            while (prevTarget == nextTarget)
            {
                nextTarget = Random.Range(0, targets.Length);
            }
            prevTarget = nextTarget;
        }

        targetRelPos = targets[nextTarget].transform.position - transform.position;


        return targetRelPos;
    }

    // Set Move in action
    void SetMove(Vector3 targetRelPos)
    {
        Quaternion lookRotation = Quaternion.LookRotation(-targetRelPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


    void SetIsEating(bool isHungry)
    {
        isEating = isHungry;
    }
}
