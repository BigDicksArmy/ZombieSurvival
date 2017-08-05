using UnityEngine;
using Player;
using System;

public class Collect : MonoBehaviour
{
	private Vector3 LastWeaponPlace;
	private GameObject weaponPrefab;
	private string objectName;
	private Sprite weaponSprite;
	private SpriteRenderer spriteRenderer;
	private GameObject WeaponPlace;
	private Equipment eq;
	private void Awake()
	{
		objectName = gameObject.name.RemoveAfter('_'); //Get the name of the weapon i.e. shotgun_collectable is a shotgun
		weaponPrefab = Objects.WeaponList.Find( gm => gm.name == objectName ); //find the weapon with the specified name
		eq = FindObjectOfType<Equipment>(); //Players equipment
		WeaponPlace = GameObject.Find("WeaponPlace"); //Where the weapon sprite should go
		spriteRenderer = WeaponPlace.GetComponent<SpriteRenderer>();    //sprite renderer of the gm where the weapon should be placed
	}

	private void Start()
	{
		weaponSprite = weaponPrefab.GetComponent<SpriteRenderer>().sprite; //sprite which represents the weapon
		LastWeaponPlace = WeaponPlace.transform.localPosition;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			eq.Add(weaponPrefab);									//Add the weapon to the equipment of the player
			WeaponPlace.transform.localPosition = LastWeaponPlace;  //Reset the position of the weapon place to the original position

			//Move the weaponplace object to a position that looks like the player is holding the weapon
			Vector3 offset = weaponPrefab.transform.Find("GripSpot").position - weaponPrefab.transform.position;
			WeaponPlace.transform.localPosition -= offset;

			spriteRenderer.sprite = weaponSprite;					//Change the sprites
			Destroy(gameObject);									//Destroy the object that was collected
		}
	}
}
