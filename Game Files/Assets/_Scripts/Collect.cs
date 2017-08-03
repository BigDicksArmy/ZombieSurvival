using UnityEngine;
using Player;

public class Collect : MonoBehaviour
{
	private GameObject weaponPrefab;
	private string objectName;
	private Sprite weaponSprite;
	private SpriteRenderer spriteRenderer;
	private GameObject WeaponPlace;
	private Equipment eq;

	private void Awake()
	{
		eq = FindObjectOfType<Equipment>();
		WeaponPlace = GameObject.Find("WeaponPlace");
		spriteRenderer = WeaponPlace.GetComponent<SpriteRenderer>();
		objectName = gameObject.name.RemoveAfter('_');
		weaponPrefab = Objects.Weapons[objectName];
		weaponSprite = weaponPrefab.GetComponent<SpriteRenderer>().sprite;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			spriteRenderer.sprite = weaponSprite;
			Vector3 offset = weaponPrefab.transform.Find("GripSpot").position + weaponPrefab.transform.position;
			WeaponPlace.transform.localPosition -= offset;
			eq.Add(weaponPrefab);
			Destroy(gameObject); //destroy the object that was collected
		}
	}

}
