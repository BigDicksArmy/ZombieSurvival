using UnityEngine;

public class Weapons
{
	//Stores Game Objects which are representations of weapons
	//not a list but list sounds better than array
	public GameObject[] WeaponList;

	public string[] WeaponNames;		//For keeping the weapon names public

	//Indexer to acces the game objects by names 
	//Implemented using a singleton
	public GameObject this[string name]
	{
		get
		{
			for (int i = 0; i < WeaponList.Length; i++)
			{
				if (name == WeaponList[i].name)
				{
					return WeaponList[i];
				}
			}
			return null;
		}
	}

	public GameObject this[int index]
	{
		get
		{
			return WeaponList[index];
		}
		set
		{
			WeaponList[index] = value;
		}
	}
}

public class Bullets
{
	//Stores Game Objects which are representations of bullets
	public GameObject[] BulletList;

	//Indexer to acces the game objects by names
	public GameObject this[string name]
	{
		get
		{
			for (int i = 0; i < BulletList.Length; i++)
			{
				if (name == BulletList[i].name)
				{
					return BulletList[i];
				}
			}
			return null;
		}
	}
	public GameObject this[int index]
	{
		get
		{
			return BulletList[index];
		}
	}
}

public class Objects : MonoBehaviour
{
	public static Weapons Weapons = new Weapons();

	//Path to each weapon in the Resource folder 
	public string[] WeaponPathList;

	public static Bullets Bullets = new Bullets();

	//Path to each weapon in the Resource folder 
	public string[] BulletPathList;

	private void Awake()
	{
		Weapons.WeaponList = new GameObject[WeaponPathList.Length];

		for (int i = 0; i < WeaponPathList.Length; i++)
		{
			Weapons.WeaponList[i] = Load("_Prefabs/Weapons/" + WeaponPathList[i]);// in the path put only what comes after Assets/Resources/ and don't put in the file extension
		}

		Bullets.BulletList = new GameObject[BulletPathList.Length];

		for (int i = 0; i < BulletPathList.Length; i++)
		{
			Bullets.BulletList[i] = Load("_Prefabs/Weapons/" + BulletPathList[i]);
		}

		Weapons.WeaponNames = new string[Weapons.WeaponList.Length];

		for (int i = 0; i < Weapons.WeaponList.Length; i++)
		{
			Weapons.WeaponNames[i] = Weapons.WeaponList[i].gameObject.name;
		}
	}

	//Only to simplify the expressions
	private GameObject Load(string Path)
	{
		return Resources.Load(Path, typeof(GameObject)) as GameObject;
	}
}
