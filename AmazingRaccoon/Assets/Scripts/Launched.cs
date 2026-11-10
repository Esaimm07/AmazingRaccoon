using UnityEngine;
using UnityEngine.XR;

public class Launched : MonoBehaviour
{
    Rigidbody2D rb;
    bool launch = false;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            launch = true;
        } 
    }

    void FixedUpdate()
    {
        if (launch)
        {
            rb.AddForce(new Vector2(speed, speed));
            launch = false;
        }

    }

    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }

}
