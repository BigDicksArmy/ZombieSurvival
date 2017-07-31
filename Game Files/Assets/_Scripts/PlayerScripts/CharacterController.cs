using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;

	public bool canGoUp;

	private float vertical_velocity;
	private float save_gravity;
	//public GameObject Prefab;
	//public GameObject SpawnPosition;
	void Start()
	{
		//Physics2D.gravity = Vector2.zero;
		rb = GetComponent<Rigidbody2D>();
		save_gravity = rb.gravityScale;
	}

	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		var horizontal_speed = speed * h;
		rb.velocity = new Vector2(horizontal_speed, 0);

		if (canGoUp)
		{
			rb.gravityScale = 0f;

			vertical_velocity = speed / 2 * Input.GetAxisRaw("Vertical");
			rb.velocity = new Vector2(horizontal_speed, vertical_velocity);
		}
		else
		{
			rb.gravityScale = save_gravity;
		}

		/*
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			rb.velocity = new Vector2(-speed, 0);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			rb.velocity = new Vector2(speed, 0);
		}
        */
		//if (Input.GetKeyUp(KeyCode.Space))
		//{
		//	Instantiate(Prefab, SpawnPosition.transform.position, Quaternion.identity);
		//}
	}
}
