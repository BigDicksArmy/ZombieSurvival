using UnityEngine;
using Utility;
using System;

public class ItemPickup : Interactable
{
	private Weapon obj;

	private void Awake()
	{
		collider = GetComponent<CircleCollider2D>();
		collider.radius = interactionRadius;
	}
	private void Start()
	{
		try
		{
			obj = EquipmentController.Instance.Inventory.Find(name);
		}
		catch (NullReferenceException)
		{
			Debug.LogError("Attach the weapon to the player under WeaponHolder object");
		}
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerHB"))
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
