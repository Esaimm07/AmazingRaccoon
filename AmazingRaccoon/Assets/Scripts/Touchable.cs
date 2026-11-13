using UnityEngine;

public class Touchable : MonoBehaviour
{
    public BoxCollider2D horizontalCollider, verticalCollider;

    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.col)
    }
}
