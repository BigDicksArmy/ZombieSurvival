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

		//PUBLIC VARIABLES
		public int EquipmentSize;
		public int WeaponsCount;
		public GameObject CurrentWeapon;

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

		//PUBLIC METHODS
		public void Add(GameObject obj)
		{
			CurrentWeapon = obj;
			Shooting sh;
			var comp = WeaponPlace.GetComponent<Shooting>();
			if (FirstWeapon == null)
			{
				FirstWeapon = obj;
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
		void CopyComponent(Shooting thisShooting, Shooting shooting)
		{
			thisShooting.MagazineSize = shooting.MagazineSize;
			thisShooting.MagazineCount = shooting.MagazineCount;
			thisShooting.BulletSpeed = shooting.BulletSpeed;
			thisShooting.BulletsLeft = shooting.BulletsLeft;
			thisShooting.bulletSpawn = shooting.bulletSpawn;
			thisShooting.FireRate = shooting.FireRate;
			thisShooting.firemode = shooting.firemode;
			thisShooting.ReloadSpeed = shooting.ReloadSpeed;
		}
		#endregion

		//PRIVATE METHODS
		void Awake()
		{
			WeaponPlace = GameObject.Find("WeaponPlace");
			spriteRenderer = WeaponPlace.GetComponent<SpriteRenderer>();
		}
		void Start()
		{
			LastWeaponPlace = WeaponPlace.transform.localPosition;
			WeaponsCount = 0;
		}
		void SelectWeapon(GameObject weapon)
		{
			CurrentWeapon = weapon;
			WeaponPlace.transform.localPosition = LastWeaponPlace;

			spriteRenderer.sprite = weapon.GetComponent<SpriteRenderer>().sprite;

			Vector3 offset = weapon.transform.Find("GripSpot").position + weapon.transform.position;
			WeaponPlace.transform.localPosition -= offset;

		}
		void Update()
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
	}
}