using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Statistics //Strenght,Endurance,Intelligence,Luck
{
	S = 1, E, I, L
}
public class Player
{
	//Private Variables
	private static Player player;
	private uint level;
	private string name;

	//Public Variables
	public Equipment Equipment;
	public GameObject Prefab;

	//Constructors
	private Player()
	{

	}

	//Properties
	public static Player P
	{
		get
		{
			if (P == null)
			{
				return player = new Player();
			}
			return player;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
	}

	//Methods
	//public void LevelUp()
	//{

	//}

	//Inertial classes
	//static class PlayerStats
	//{
	//	private static Dictionary<Statistics, uint> stats = new Dictionary<Statistics, uint>();

	//	public static Statistics Skills;
	//}
}



