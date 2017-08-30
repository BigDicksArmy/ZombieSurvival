using System.Collections.Generic;
using UnityEngine;
using Utility;

public delegate AudioClip EquipmentDel();
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

	public List<Weapon> Inventory;
	public int equipmentSize = 0;
	public Weapon Current;
	public EquipmentDel WeaponChanged;

	private int weaponsCount;
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
			if (Inventory.Count == EquipmentSize)
				return true;
			return false;
		}
	}

	public int EquipmentSize
	{
		get {	return equipmentSize;}
		set {	equipmentSize = value;	}
	}

	public int WeaponsCount
	{
		get {	return weaponsCount;	}
		set {	weaponsCount = value;	}
	}
	#endregion

	#region Add,Remove Function upgrade them
	public bool Add(Weapon item)
	{
		if (!IsFull)
		{
			Inventory.Add(item);
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
			Inventory.Remove(item);
		}
	}
	#endregion
	//PRIVATE METHODS
	void Start()
	{
		Inventory = new List<Weapon>();
		WeaponHolder = transform.Find("WeaponHolder");
		foreach (Transform child in WeaponHolder)
		{
			//Debug.Log(child.name);
			Weapon tmp = new Weapon(child.gameObject, false);
			Inventory.Add(tmp);
		}
		WeaponsCount = 0;
		Current = Inventory.Find("Empty");     //Selecting the empty weapon
	}
	void Update()
	{
		if (!IsEmpty)
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SelectWeapon(Inventory[0]);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				SelectWeapon(Inventory[1]);
			}
		}
	}

	#region Weapon Selection
	public void SelectWeapon(Weapon obj)
	{
		if (!obj.IsCollected)   //If its not collected then it cannot be selected
		{
			Debug.LogError("Weapon is not collected");
			return;
		}
		else
		{
			Current = obj;
			Current.Firearm.SetActive(true);
			for (int i = 0; i < Inventory.Count; i++)
			{
				if (Inventory[i].IsCollected && Inventory[i].Firearm.name != "Empty" && Inventory[i].Firearm.name != Current.Firearm.name)
				{
					Inventory[i].Firearm.SetActive(false);
				}
			}
		}
	}
	#endregion
}