using UnityEngine;

namespace Player
{
	class Equipment : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		private GameObject WeaponPlace;
		private Vector3 LastWeaponPlace;

		//PUBLIC VARIABLES
		public int EquipmentSize;
		public int WeaponsCount;
		public GameObject[] Weapons;

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
			if (!Search(obj))
			{
				WeaponsCount++;
				if (WeaponsCount - 1 < EquipmentSize)
				{
					Weapons[WeaponsCount - 1] = obj;
				}
				else
				{
					WeaponsCount = EquipmentSize;
				}
			}
		}

		public bool Search(GameObject obj)
		{
			for (int i = 0; i < EquipmentSize; i++)
			{
				if (Weapons[i] == obj)
				{
					return true;
				}
			}
			return false;
		}

		public GameObject Search(string name)
		{
			for (int i = 0; i < EquipmentSize; i++)
			{
				if (Weapons[i].name == name)
				{
					return Weapons[i];
				}
			}
			return null;
		}

		//PRIVATE METHODS
		private void Awake()
		{
			WeaponPlace = GameObject.Find("WeaponPlace");
			spriteRenderer = WeaponPlace.GetComponent<SpriteRenderer>();
		}
		private void Start()
		{
			LastWeaponPlace = WeaponPlace.transform.localPosition;
			Weapons = new GameObject[EquipmentSize];
			WeaponsCount = 0;
		}
		private void SelectWeapon(string name) 
		{			
			for (int i = 0; i < Weapons.Length; i++)
			{
				if (Weapons[i].name == name)
				{
					WeaponPlace.transform.position = LastWeaponPlace;
					spriteRenderer.sprite = Weapons[i].GetComponent<SpriteRenderer>().sprite;
					Vector3 offset = Weapons[i].transform.Find("GripSpot").position + Weapons[i].transform.position;
					WeaponPlace.transform.localPosition -= offset;
					break;
				}
			}
		}
		private void Update()
		{
			if (!IsEmpty)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1) && Search("Shotgun")) //null reference exc when less than one weapon propably smth 2 do with the search()
				{
					SelectWeapon("Shotgun");
				}
				if (Input.GetKeyDown(KeyCode.Alpha2) && Search("Uzi"))
				{
					SelectWeapon("Uzi");
				}
			}
		}
	}
}