using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public uint BulletsLeft;
	public uint MagazineSize;
	public uint MagazineCount;
	public float BulletSpeed;

	private Player.Equipment equipment;
	private GameObject Bullet;
	private void Awake()
	{
		Bullet = Objects.Bullet;
		equipment = transform.parent.GetComponent<Player.Equipment>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (BulletsLeft > 0)
			{
				GameObject bullet = Instantiate(Bullet, equipment.CurrentBulletSpawn, transform.rotation, null); //repair this shit
				//bullet.transform.
				//Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
				//rb.velocity = new Vector2(BulletSpeed,rb.velocity.y);
				BulletsLeft--;
			}
		}
		if (Input.GetKeyDown(KeyCode.R) &&BulletsLeft >= 0 && BulletsLeft <= MagazineSize)
		{
			Reload();
		}
	}
	private void Reload()
	{
		if (MagazineCount > 0)
		{
			BulletsLeft = MagazineSize;
			MagazineCount--;
		}
	}
}
