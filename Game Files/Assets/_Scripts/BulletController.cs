using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletController : MonoBehaviour
{
	new Collider2D collider;
	void Awake()
	{
		collider = GetComponent<Collider2D>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collider.isTrigger = false;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collider.isTrigger = true;
		}
	}
}
