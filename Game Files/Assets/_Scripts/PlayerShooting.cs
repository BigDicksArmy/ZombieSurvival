using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	private GameObject weaponPrefab;
	private GameObject bulletPrefab;
	private Transform bulletSpawn;

	public float BulletSpeed;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Transform weaponPlace = GetComponentInChildren<Transform>();
		GameObject weapon = new GameObject();
		weapon = collision.gameObject.GetComponentInChildren<GameObject>(true);
		Component[] components = weapon.GetComponentsInChildren<Transform>();
		//unfinished
	}

	private void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * BulletSpeed;

		// Destroy the bullet after 2 seconds
		//Destroy(bullet, 2.0f);
	}
}
