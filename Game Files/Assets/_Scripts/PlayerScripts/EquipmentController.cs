using System.Collections.Generic;
using UnityEngine;

class Weapon
{
	public GameObject Firearm;
	public bool IsCollected;

	public Weapon(GameObject firearm, bool isCollected)
	{
		Firearm = firearm;
		IsCollected = isCollected;
	}
}
class EquipmentController : MonoBehaviour
{
	#region Singleton
	private static EquipmentController _instance;

	public static EquipmentController Instance
	{
		get
		{ return _instance; }

		private set
		{ _instance = value; }
	}

	void Awake()
	{
		if (_instance != null)
		{
			Debug.LogError("More than one instance of a singleton detected");
			return;
		}
		_instance = this;
	}
	#endregion

	//public delegate void OnEquipmentChanged();
	//public OnEquipmentChanged onEquipmentChangedCallback;

	public List<Weapon> inventory;
	public int EquipmentSize = 0;
	public int WeaponsCount;
	public Weapon Current;

	private Transform WeaponHolder;

	#region Properties
	public bool IsEmpty
	{
		get
		{
			if (WeaponsCount == 0)
				return true;
			return false;
		}
	}
	public bool IsFull
	{
		get
		{
			if (WeaponsCount == EquipmentSize)
				return true;
			return false;
		}
	}
	#endregion
	#region Add,Remove Function upgrade them
	public bool Add(Weapon item)
	{
		if (!IsFull)
		{
			inventory.Add(item);
			//if (onEquipmentChangedCallback != null)
			//	onEquipmentChangedCallback.Invoke();
			return true;
		}
		return false;
	}
	private void Remove(Weapon item)
	{
		if (!IsEmpty)
		{
			inventory.Remove(item);
		}
	}
	#endregion
	//PRIVATE METHODS
	void Start()
	{
		inventory = new List<Weapon>();
		WeaponHolder = transform.Find("WeaponHolder");
		foreach (Transform child in WeaponHolder)
		{
			Debug.Log(child.name);
			Weapon tmp = new Weapon(child.gameObject, false);
			inventory.Add(tmp);
		}
		WeaponsCount = 0;
		Current = inventory.Find(gm => gm.Firearm.name == "Empty");     //Selecting the empty weapon
	}
	public delegate int Operator(int i);
	void SelectWeapon(Operator operation)
	{
		for (int i = 0; i < inventory.Count; operation(i))
		{
			if (i < 0)
				i = inventory.Count - 1;
			if (i == inventory.Count - 1)
				i = 0;
			if (inventory[i].Firearm.name != "Empty" && inventory[i].IsCollected)
			{
				if (inventory[i].Firearm.name == Current.Firearm.name)
				{
					if (i < 0)
						i = inventory.Count - 1;
					if (i == inventory.Count - 1)
						i = 0;
					Current = inventory[i];
					Current.Firearm.SetActive(true);
					return;
				}
				inventory[i].Firearm.SetActive(false);
			}
		}
		Debug.Log("No weapon found");
	}
	void Update()
	{
		if (!IsEmpty)
		{
			if (Input.GetAxis("Mouse ScrollWheel") > 0)
			{
				SelectWeapon(x => x++);
			}
			if (Input.GetAxis("Mouse ScrollWheel") < 0)
			{
				SelectWeapon(x => x--);
			}
		}
	}
}