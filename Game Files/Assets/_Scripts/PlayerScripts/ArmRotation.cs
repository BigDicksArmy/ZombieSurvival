using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
	private Camera mainCamera;
	private Transform playerTransform;
	private Weapon weapon;

	void Update()
	{
		Aim();
	}

	private void Aim()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5f;

		Vector3 objectPos = mainCamera.WorldToScreenPoint(playerTransform.position);

		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
}
