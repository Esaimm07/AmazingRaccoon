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



        if (jumpPressed && onWall)
        {
            rb.linearVelocity = new Vector2(jumpForce, jumpForce);
            sliding = false;
            print("wall jump");

        }
        else if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
            print("normal jump");
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
        if (collision.transform.tag == "VerticalWall")
        {
            if (!onWall)
            {
                print("On wall");
                sliding = true;
            }
            rb.gravityScale = 0;
            onWall = true;
        }
        if (collision.transform.tag == "Floor")
        {
            if (!onFloor)
            {
                print("On floor");
            }
            onFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "VerticalWall")
        {
            if (onWall)
            {
                print("Off wall");
                sliding = false;
            }
            rb.gravityScale = gravity;
            onWall = false;
        }
        if (collision.transform.tag == "Floor")
        {
            if (onFloor)
            {
                print("Off floor");
            }
            onFloor = false;
        }
    }

}
