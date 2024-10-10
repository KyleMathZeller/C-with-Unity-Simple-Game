using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public variable mean the variable can be edited and updated in the insepctor window
    public static PlayerController instance;
    public float jumpForce = 6f;
    public float runningSpeed = 1.5f;
    //Allows us to point at other items for reference in the inspector. Needed for the raycast and "this" function to target properly
    public Animator animator;
    private Vector3 startingPosition;
    private Rigidbody2D rigidBody;

    // This code is used for initialization
    void Awake()
    {
        instance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = this.transform.position;
    }

    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        this.transform.position = startingPosition;
    }
    // This is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            //~~~NOTE~~~
            //This is our input method, change later to phone compatiable method
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
            animator.SetBool("isGrounded", isGrounded());
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }
        }
    }
    void Jump()
    {
        if (isGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public LayerMask groundLayer;

    bool isGrounded()
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
    public void Kill()
    {
        GameManager.instance.GameOver();
        animator.SetBool("isAlive", false);

        if (PlayerPrefs.GetFloat("highscore", 0) < this.GetDistance())
        {
            PlayerPrefs.SetFloat("highscore", this.GetDistance());
        }
    }

    public float GetDistance()
    {
        float traveledDistance = Vector2.Distance(new Vector2(startingPosition.x, 0), new Vector2(this.transform.position.x, 0));

        return traveledDistance;
    }
}
