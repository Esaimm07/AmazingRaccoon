using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public float jumpForce = 5,horizontal,speed;
    public bool jumpPressed = false, onWall = false,onFloor = false;
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
        rb.linearVelocity = new Vector2(
            horizontal * speed,
            rb.linearVelocity.y
        );
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "VerticalWall")
        {
            if (!onWall)
            {
                print("On wall");
            }
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
