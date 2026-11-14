using UnityEngine;

public class VerticalCollider : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            player.onFloor = true;
            player.sliding = false;
            player.onWall = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Obstacle")
        {
            player.onFloor = false;
        }
    }
}
