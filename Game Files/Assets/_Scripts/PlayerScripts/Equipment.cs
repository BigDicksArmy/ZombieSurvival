using System;
using System.Reflection;
using UnityEngine;

namespace Player
{
	class Equipment : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		private GameObject WeaponPlace;
		private Vector3 LastWeaponPlace;
		private GameObject FirstWeapon;
		private GameObject SecondWeapon;
		private Vector3 FirstBulletSpawn;
		private Vector3 SecondBulletSpawn;


		//PUBLIC VARIABLES
		public int EquipmentSize;
		public int WeaponsCount;
		public GameObject CurrentWeapon;
		public Vector3 CurrentBulletSpawn;

		#region Eq Managment
		//PUBLIC PROPERTIES
		public bool IsEmpty
		{
			get
			{
				if (WeaponsCount == 0)
				{
					return true;
				}
				return false;
			}
		}

		public bool IsFull
		{
			get
			{
				if (WeaponsCount == EquipmentSize)
				{
					return true;
				}
				return false;
			}
		}

		//PUBLIC METHODS
		public void Add(GameObject obj)
		{
			CurrentWeapon = obj;
			CurrentBulletSpawn = ReturnBulletSpawn(obj);
			Shooting sh;
			var comp = WeaponPlace.GetComponent<Shooting>();
			if (FirstWeapon == null)
			{
				FirstWeapon = obj;
				FirstBulletSpawn = FirstWeapon.transform.Find("BulletSpawn").localPosition;
				sh = FirstWeapon.GetComponent<Shooting>();
				if (comp != null)
				{
					CopyComponent(comp, sh);
				}
				else
				{
					comp = WeaponPlace.AddComponent<Shooting>();
					CopyComponent(comp, sh);
				}
				WeaponsCount++;
			}
			else if (SecondWeapon == null && FirstWeapon != null)
			{
				SecondWeapon = obj;
				SecondBulletSpawn = SecondWeapon.transform.Find("BulletSpawn").localPosition;
				sh = SecondWeapon.GetComponent<Shooting>();
				if (comp != null)
				{
					CopyComponent(comp, sh);
				}
				else
				{
					comp = WeaponPlace.AddComponent<Shooting>();
					CopyComponent(comp, sh);
				}
				WeaponsCount++;
			}
			else
			{
				Debug.Log("No space for another weapon");
			}
		}
		//Copies some important values to another component
		private void CopyComponent(Shooting thisShooting, Shooting shooting)
		{
			thisShooting.MagazineSize = shooting.MagazineSize;
			thisShooting.MagazineCount = shooting.MagazineCount;
			thisShooting.BulletSpeed = shooting.BulletSpeed;
			thisShooting.BulletsLeft = shooting.BulletsLeft;
		}
		#endregion

		//PRIVATE METHODS
		private void Awake()
		{
			WeaponPlace = GameObject.Find("WeaponPlace");
			spriteRenderer = WeaponPlace.GetComponent<SpriteRenderer>();
		}
		private void Start()
		{
			LastWeaponPlace = WeaponPlace.transform.localPosition;
			WeaponsCount = 0;
		}
		private void SelectWeapon(GameObject weapon)
		{
			CurrentWeapon = weapon;
			CurrentBulletSpawn = ReturnBulletSpawn(weapon);
			WeaponPlace.transform.localPosition = LastWeaponPlace;
			spriteRenderer.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
			Vector3 offset = weapon.transform.Find("GripSpot").position + weapon.transform.position;
			WeaponPlace.transform.localPosition -= offset;

		}
		private void Update()
		{
			if (!IsEmpty)
			{
				//PUT HERE EVERY 
				if (Input.GetKeyDown(KeyCode.Alpha1) && (FirstWeapon != null)) 
				{
					SelectWeapon(FirstWeapon);
					Shooting sh = CurrentWeapon.GetComponent<Shooting>();
					CopyComponent(WeaponPlace.GetComponent<Shooting>(), sh);
				}
				if (Input.GetKeyDown(KeyCode.Alpha2) && (SecondWeapon != null))
				{
					SelectWeapon(SecondWeapon);
					Shooting sh = CurrentWeapon.GetComponent<Shooting>();
					CopyComponent(WeaponPlace.GetComponent<Shooting>(), sh);
				}
			}
		}
		private Vector3 ReturnBulletSpawn(GameObject obj)
		{
			return obj.transform.Find("BulletSpawn").localPosition;
		}
	}
}