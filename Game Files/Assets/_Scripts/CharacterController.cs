using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public float speed;
	private Rigidbody2D rb;
	public GameObject Prefab;
	public GameObject SpawnPosition;
	void Start()
	{
		Physics2D.gravity = Vector2.zero;
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			rb.velocity = new Vector2(-speed, 0);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			rb.velocity = new Vector2(speed, 0);
		}
		//if (Input.GetKeyUp(KeyCode.Space))
		//{
		//	Instantiate(Prefab, SpawnPosition.transform.position, Quaternion.identity);
		//}
	}
}
