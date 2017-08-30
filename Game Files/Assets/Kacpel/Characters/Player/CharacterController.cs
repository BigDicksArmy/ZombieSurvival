using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;

    public bool canGoUp;
    public bool changed_closest_waypoint;

    public GameObject closest_waypoint;

    private float vertical_velocity;
    private float save_gravity;

    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
        save_gravity = rb.gravityScale;
	}

	void FixedUpdate()
	{
        float h = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * h, rb.velocity.y);

        if (canGoUp) // na schodach
        {
            rb.gravityScale = 0f;

            vertical_velocity = speed / 2 * Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical_velocity);
        }
        else
        {
            rb.gravityScale = save_gravity;
        }
    }
}
