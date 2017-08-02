using System.Linq;
using UnityEngine;

public class Collect : MonoBehaviour
{
	public GameObject WeaponPlace;

	private GameObject weaponPrefab;
	private string objectName;
	private Sprite weaponSprite;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = WeaponPlace.GetComponent<SpriteRenderer>();
		objectName = gameObject.name.RemoveAfter('_');
		weaponPrefab = Equipment.Weapons[objectName];
		weaponSprite = weaponPrefab.GetComponent<SpriteRenderer>().sprite;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			spriteRenderer.sprite = weaponSprite;
			
			Destroy(this.gameObject); //destroy the object that was collected
		}
	}

}
