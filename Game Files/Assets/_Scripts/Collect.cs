using UnityEngine;

public class Collect : MonoBehaviour
{
	public bool IsCollected = false;
	private GameObject weaponPrefab;
	private GameObject bulletPrefab;
	private Transform gripSpot;
	private Transform bulletSpawn;
	private Transform weaponPlace;
	// Use this for initialization
	private void Start()
	{
		//Getting the game objects from weapon childs
		weaponPrefab = transform.Find("Weapon").gameObject;
		bulletPrefab = transform.Find("Bullet").gameObject;
		gripSpot = weaponPrefab.transform.Find("GripSpot");
		bulletSpawn = weaponPrefab.transform.Find("BulletSpawn");
		//Debug.Log(weaponPrefab.name);
		//Debug.Log(bulletPrefab.name);
		//Debug.Log(gripSpot.name);
		//Debug.Log(bulletSpawn.name);
		//works
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			weaponPrefab.GetComponent<SpriteRenderer>().enabled = true;
			weaponPlace = collision.gameObject.transform.Find("WeaponPlace");
			//Debug.Log(weaponPlace.name);
			var wp = Instantiate(weaponPrefab, collision.transform);
			wp.transform.localPosition = weaponPlace.localPosition;
			Destroy(this.gameObject);
		}
	}

}
