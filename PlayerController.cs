using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public variable mean the variable can be edited and updated in the insepctor window
    public float jumpForce = 6f;
    public float runningSpeed = 1.5f;

    private Rigidbody2D rigidBody;
    //Allows us to point at other items for reference in the inspector. Needed for the raycast and "this" function to target properly
    public Animator animator;
    // This code is used for initialization
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator.SetBool("isAlive", true);
    }
    // This is called once per frame
    void Update()
    {
        //~~~NOTE~~~
        //This is our input method, change later to phone compatiable method
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        animator.SetBool("isGrounded", IsGrounded());
    }
    void FixedUpdate()
    {
        if (rigidBody.velocity.x < runningSpeed)
        {
            rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
        }
    }
    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public LayerMask groundLayer;

    bool IsGrounded()
    {
        //Raycast method goes from source, attached object, in a direction and report what it has hit
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
