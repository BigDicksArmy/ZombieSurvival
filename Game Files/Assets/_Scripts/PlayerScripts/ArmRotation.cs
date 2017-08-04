using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
	public float rotationOffset;

	private Camera mainCamera;
	private Transform ArmTransform;
	private SpriteRenderer spriteRend;
	private bool isFlipped;

	private void Awake()
	{
		spriteRend = GetComponent<SpriteRenderer>();
		mainCamera = Camera.main;
		ArmTransform = transform;
	}
	private void Start()
	{
	}
	void Update()
	{
		Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - ArmTransform.localPosition;
		difference.Normalize();

		float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + rotationOffset;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
		//Flipping the sprites of the player and the weapon
		if (Mathf.Abs(rotationZ) > 90 && !isFlipped)
		{
			isFlipped = true;
			spriteRend.flipY = true;
		}
		if (Mathf.Abs(rotationZ) < 90 && isFlipped)
		{
			isFlipped = false;
			spriteRend.flipY = false;
		}
	}
}
