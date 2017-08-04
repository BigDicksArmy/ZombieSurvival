using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public uint MagazineSize;
	public uint MagazineCount;
	public float BulletSpeed;

	private uint bulletsLeft;

	private void Awake()
	{

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (bulletsLeft > 0)
			{
				//Instantiate(Objects.Bullets[weaponName]);
				//weapon.shot();
				bulletsLeft--;
			}
		}
		if (bulletsLeft >=0 && bulletsLeft <=MagazineSize)
		{
			Reload();
		}
	}
	private void Reload()
	{
		if (MagazineCount > 0)
		{
			bulletsLeft = MagazineSize;
			MagazineCount--;
		}
	}
}
