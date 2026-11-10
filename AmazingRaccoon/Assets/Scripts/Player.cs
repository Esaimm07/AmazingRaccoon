using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float jumpForce = 5, horizontal, speed, gravity = 2,slidingSpeed, acc,decc;
    public bool jumpPressed = false, onWall = false, onFloor = false, sliding = false;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && ((onWall == true) || (onFloor == true)))
        {
            jumpPressed = true;
        }
        
    }

    void FixedUpdate()
    {
        if (sliding == true && jumpPressed == false)
        {
            Sliding();
        }
        Movement();

    }

    void Movement()
    {
        #region Main Move
        float targetSpeed = horizontal * speed;
        float rate;

        if (horizontal != 0)
        {
            rate = acc;
        }
        else
        {
            rate = decc;
        }

        float newVelocityX = Mathf.MoveTowards(rb. linearVelocity.x, targetSpeed, rate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);
        #endregion

        if (jumpPressed && onWall)
        {
            rb.linearVelocity = new Vector2(jumpForce, jumpForce);
            sliding = false;

        }
        else if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    void Sliding()
    {
        if (Input.GetKey(KeyCode.W))
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slidingSpeed/3);
        else
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slidingSpeed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            if (onWall)
            {
                sliding = true;
                rb.gravityScale = 0;
                onFloor = false;
            }         
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!onWall && collision.transform.tag == "Obstacle")
        {
            onFloor = true;
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
        if(collision.tag == "Obstacle")
        {
            onWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            onWall = false;
            onFloor = false;
            rb.gravityScale = gravity;
            sliding = false;
        }
    }

}
