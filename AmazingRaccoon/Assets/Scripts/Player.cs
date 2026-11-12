using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f, horizontal, speed = 7.5f, gravity = 2f,slidingSpeed = 2f;
    public float acc = 60f, decc = 15f, sprintSpeed = 10f, sprintAcc = 75f, currentSpeed = 7.5f, currentAcc = 60f;
    public bool jumpPressed = false, onWall = false, onFloor = false, sliding = false, lookingLeft;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        DirectionLookAndWallJump();
        Sprint();
    }

    void FixedUpdate()
    {
        Sliding();       
        Movement();
    }

    void DirectionLookAndWallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ((onWall == true) || (onFloor == true)))
        {
            jumpPressed = true;
        }

        if (horizontal == -1 && !sliding)
            lookingLeft = true;
        else if (horizontal == 1 && !sliding)
            lookingLeft = false;
    }
    void Movement()
    {
        #region Main Move
        float targetSpeed = horizontal * currentSpeed;
        float rate;

        if (horizontal != 0)
        {
            rate = currentAcc;
        }
        else
        {
            rate = decc;
        }

        float newVelocityX = Mathf.MoveTowards(rb. linearVelocity.x, targetSpeed, rate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);
        #endregion

        #region Jumps
        if (jumpPressed && onWall && lookingLeft)
        {
            rb.linearVelocity = new Vector2(jumpForce/2, jumpForce);
            sliding = false;
        }
        else if (jumpPressed && onWall && !lookingLeft)
        {
            rb.linearVelocity = new Vector2(-jumpForce/2, jumpForce);
            sliding = false;
        }
        else if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
        #endregion
    }

    void Sliding()
    {
        if (sliding == true && jumpPressed == false)
        {
            if (Input.GetKey(KeyCode.W))
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slidingSpeed / 3);
            else
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slidingSpeed);
        }
        
    }

    void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            currentAcc = sprintAcc;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = speed;
            currentAcc = acc;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            if (!onWall)
            {
                onFloor = true;
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!onWall && collision.transform.tag == "Obstacle")
        {
            onFloor = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle" && !onFloor)
        {
            onWall = true;
            sliding = true;
            rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" && !onFloor)
        {
            onWall = false;
            rb.gravityScale = gravity;
            sliding = false;
        }
    }

}
