using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float jumpForce = 5,horizontal,speed,gravity = 2,slidingSpeed;
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
        Movement();
        if (sliding == true && jumpPressed == false)
        {
            Sliding();
        }
        
    }

    void Movement()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            speed += horizontal * speed;
            rb.linearVelocity = new Vector2(Mathf.Clamp(((horizontal * speed) + horizontal),-5,5) , rb.linearVelocity.y);

        }

        if (jumpPressed && onWall)
        {
            rb.linearVelocity = new Vector2(jumpForce, jumpForce);
            jumpPressed = false;
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
        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, -slidingSpeed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "VerticalWall")
        {
            if (!onWall)
            {
                print("On wall");
            }
            sliding = true;
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
            }
            sliding = false;
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
