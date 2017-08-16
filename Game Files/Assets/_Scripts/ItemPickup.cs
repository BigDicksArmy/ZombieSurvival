using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemPickup : Interactable
{
	Weapon obj;
	private void Awake()
	{
		collider = GetComponent<CircleCollider2D>();
		collider.radius = interactionRadius;
	}
	private void Start()
	{
		try
		{
			obj = EquipmentController.Instance.Inventory.Find(x => x.Firearm.name == name);
		}
		catch (NullReferenceException)
		{
			Debug.LogError("Attach the weapon to the player under ");
		}
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (!obj.IsCollected)
			{
				obj.IsCollected = true;
				EquipmentController.Instance.SelectWeapon(obj);
				EquipmentController.Instance.WeaponsCount++;
			}
			Destroy(gameObject);
		}
	}
}
