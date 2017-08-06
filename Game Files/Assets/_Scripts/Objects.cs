using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Objects : MonoBehaviour
{
	public static List<GameObject> WeaponList;
	public static GameObject Bullet;

	private void Awake()
	{
		WeaponList = new List<GameObject>();
		WeaponList = LoadAll("_Prefabs/Weapons/Holdable");
		Bullet = Load("_Prefabs/Weapons/Bullet");
	}

	//Only to simplify the expressions
	private List<GameObject> LoadAll(string path)
	{
		return (Resources.LoadAll(path, typeof(GameObject)).Cast<GameObject>()).ToList();
	}
	private GameObject Load(string path)
	{
		return Resources.Load(path, typeof(GameObject)) as GameObject;
	}
}
