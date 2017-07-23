using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : MonoBehaviour {

    private CharacterController thePlayer;
    public float speed;
    private Rigidbody2D rb;

    public bool canGoUp;

    private float vertical_velocity;
    private float save_gravity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<CharacterController>();

        save_gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update() {
        if (thePlayer.transform.position.x > rb.transform.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (canGoUp && thePlayer.transform.position.y > rb.transform.position.y)
        {
            rb.gravityScale = 0f;

            vertical_velocity = speed / 2;
            rb.velocity = new Vector2(0, vertical_velocity);
        }
        else if (canGoUp && thePlayer.transform.position.y < rb.transform.position.y)
        {
            rb.gravityScale = 0f;

            vertical_velocity = speed / 2;
            rb.velocity = new Vector2(0, -vertical_velocity); //rb.velocity.x,
        }
        if (!canGoUp || thePlayer.transform.position.y == rb.transform.position.y )
            rb.gravityScale = save_gravity;

    }
}
