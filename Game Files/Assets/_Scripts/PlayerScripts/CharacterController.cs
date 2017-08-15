using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public float HorizontalSpeed;
	public float VerticalSpeed;

	new private Rigidbody2D rigidbody;
	private float normalGravity;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		normalGravity = rigidbody.gravityScale;
	}
	
	void Update()
	{
		float horizontalMovement = HorizontalSpeed* Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2(horizontalMovement, 0);
		rigidbody.AddForce(movement);
	}
	
	#region OnCollison Functions
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Stairs"))
		{
			GravityOff();
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Stairs"))
		{
			GravityOn();
		}
	}
	#endregion

	#region Near stairs gravity
	void GravityOff()
	{
		rigidbody.gravityScale = 0f;
		float horizontalMovement = HorizontalSpeed * Input.GetAxis("Horizontal");
		float verticalMovement = VerticalSpeed / 2 * Input.GetAxisRaw("Vertical");
		rigidbody.AddForce(new Vector2(horizontalMovement, verticalMovement));
	}
	void GravityOn()
	{
		rigidbody.gravityScale = normalGravity;
	}
	#endregion
}
