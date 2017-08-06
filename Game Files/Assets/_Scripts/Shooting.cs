using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
	Secure = 1, Single, Burst, Automatic
}

public class Shooting : MonoBehaviour
{
	public GameObject bulletSpawn;
	public uint BulletsLeft;
	public uint MagazineSize;
	public uint MagazineCount;
	public float BulletSpeed;
	public float FireRate;
	public FireMode firemode = FireMode.Secure;
	public float ReloadSpeed;

	private Player.Equipment equipment;
	private GameObject bullet;
	private Transform weaponPlace;
	private Transform spawn;
	private float timeToFire = 0f;

	private void Awake()
	{
		bullet = Objects.Bullet;
		weaponPlace = GetComponent<Transform>();
		equipment = transform.parent.GetComponent<Player.Equipment>();
	}
	private void Start()
	{
		spawn = weaponPlace.Find("BulletSpawn");
		spawn.localPosition = equipment.CurrentWeapon.transform.localPosition + bulletSpawn.transform.localPosition;
	}

	private void Update()
	{
		switch (firemode)
		{
			case FireMode.Secure:
				//Debug.Log("Switch to another firemode");
				break;
			case FireMode.Single:
				SingleShot();
				break;
			case FireMode.Burst:
				//Debug.Log("BurstFire Still not implemented");
				break;
			case FireMode.Automatic:
				AutoShot();
				break;
			default:
				//Debug.Log("Fire mode not choosen - not good");
				break;
		}
		if (Input.GetKeyDown(KeyCode.R) && BulletsLeft >= 0 && BulletsLeft <= MagazineSize)
		{
			Reload();
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			if (firemode == FireMode.Automatic)
			{
				firemode = FireMode.Secure;
			}
			else
			{
				firemode++;
			}
			Debug.Log(firemode.ToString());
		}
	}
	#region Shooting Functions
	private void Shot()
	{
		GameObject bullet = Instantiate(this.bullet, spawn.transform.position, weaponPlace.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(new Vector2(BulletSpeed, 0f), ForceMode2D.Impulse);
		Debug.Log("Shot");
	}
	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
	}
	void Reload()
	{
		if (MagazineCount > 0 && BulletsLeft < MagazineSize)
		{
			StartCoroutine(Wait(ReloadSpeed));
			BulletsLeft = MagazineSize;
			MagazineCount--;
		}
	}

	void SingleShot()
	{
		if (Input.GetButtonDown("Fire1") && BulletsLeft > 0)
		{
			Shot();
			BulletsLeft--;
		}
	}
	void AutoShot()
	{
		if (Input.GetButton("Fire1") && Time.time > timeToFire && BulletsLeft > 0)
		{
			timeToFire = Time.time + 1 / FireRate;
			Shot();
			BulletsLeft--;
		}
	}
	#endregion
}
