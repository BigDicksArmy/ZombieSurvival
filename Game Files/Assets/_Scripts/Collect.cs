using UnityEngine;

public class Collect : MonoBehaviour
{
	private GameObject weaponPrefab;
	private GameObject bulletPrefab;
	private Transform weaponPlace;
	// Use this for initialization
	private void Awake()
	{
		weaponPrefab = transform.Find("Weapon").gameObject;
		bulletPrefab = transform.Find("Bullet").gameObject;
	}
	//private void Start()
	//{
	//	Getting the game objects from weapon childs
	//	weaponPrefab = transform.Find("Weapon").gameObject;
	//	bulletPrefab = transform.Find("Bullet").gameObject;
	//	gripSpot = weaponPrefab.transform.Find("GripSpot");
	//	bulletSpawn = weaponPrefab.transform.Find("BulletSpawn");
	//	Debug.Log(weaponPrefab.name);
	//	Debug.Log(bulletPrefab.name);
	//	Debug.Log(gripSpot.name);
	//	Debug.Log(bulletSpawn.name);
	//	works
	//}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (!Equipment.Weapons.Contains(weaponPrefab))
			{
				weaponPrefab.GetComponent<SpriteRenderer>().enabled = true; //enable the sprite renderer ot the weapon
				weaponPlace = collision.gameObject.transform.Find("WeaponPlace"); //search in the player object for a place for the weapon
																				  //Debug.Log(weaponPlace.name);
				GameObject Weapon = Instantiate(weaponPrefab, collision.transform);
				Weapon.transform.localPosition = weaponPlace.localPosition; //move the object to a specific position just for aesthetics
				Destroy(this.gameObject); //destroy the object that was collected
			}
		}
	}

}
