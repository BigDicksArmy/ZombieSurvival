using UnityEngine;

public class MovementController : MonoBehaviour
{
	public bool IsGravityOff;
	public float HorizontalSpeed;
	public float VerticalSpeed;
	public bool IsClosestWaypointChanged;
	public GameObject ClosestWaypoint;

	new private Rigidbody2D rigidbody;
	private float normalGravity;

	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		normalGravity = rigidbody.gravityScale;
	}

	void FixedUpdate()
	{
		if (CompareTag("Player"))
		{
			if (IsGravityOff)
			{
				float horizontalMovement = HorizontalSpeed * Input.GetAxis("Horizontal");
				float verticalMovement = VerticalSpeed / 2 * Input.GetAxisRaw("Vertical");
				rigidbody.velocity = new Vector2(horizontalMovement, verticalMovement);
			}
			else
			{
				float horizontalMovement = HorizontalSpeed * Input.GetAxis("Horizontal");
				rigidbody.velocity = new Vector2(horizontalMovement, rigidbody.velocity.y);
			}
		}
	}

	#region OnCollison Functions
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Stairs"))
		{
			IsGravityOff = true;
			rigidbody.gravityScale = 0f;
		}
	}
	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.CompareTag("Stairs"))
		{
			IsGravityOff = false;
			rigidbody.gravityScale = normalGravity;
		}
	}
	#endregion
}
