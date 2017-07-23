﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Object
{
	public string iD;
	public string ModelPath;
	public GameObject Model;
	public Object(string ModelPath)
	{
		iD = ItemCounter.GenerateID();
		this.ModelPath = ModelPath;
		Model = Resources.Load(ModelPath, typeof(GameObject)) as GameObject;
	}
	public override string ToString()
	{
		return "iD: " + iD.ToString();
	}
	public abstract void Use();
}
public class Weapon : Object
{
	public string Name;
	public Weapon(string ModelPath,string Name) : base(ModelPath)
	{
		this.Name = Name;
	}

	public override void Use()
	{
		
	}
}
public static class ItemCounter
{
	private static byte count;
	public static string GenerateID()
	{
		count++;
		return Convert.ToString(count, 16);
	}
}
public class InventoryException : Exception
{
	public InventoryException()
	{

	}
	public InventoryException(string Message)
		: base(Message)
	{

	}
}
public class Equipment
{
	//Private Variables
	private List<Object> eq;

	//Public Variables
	private int? sizeLimit;
	public int ObjectCount;

	//Constructors
	public Equipment()
	{
		sizeLimit = null;
		eq = new List<Object>();
	}
	public Equipment(List<Object> eq)
	{
		sizeLimit = null;
		this.eq = eq;
	}
	public Equipment(int size)
	{
		sizeLimit = size;
		eq = new List<Object>(size);
	}

	//Methods
	public bool Add(Object item)
	{
		try
		{
			add(item);
		}
		catch (InventoryException ex)
		{
			//int width = Screen.width;
			//int height = Screen.height;
			//Rect position = new Rect(width / 2 - 400, height / 2 - 300, 400, 300);
			//GUI.Box(position, ex.Message);
			EditorUtility.DisplayDialog("Inventory overload", ex.Message, "OK");
			return false;
		}
		return true;
	}
	private void add(Object item)
	{
		if (++ObjectCount > sizeLimit)
		{
			throw new InventoryException("Not enough space for another item!");
		}
		else
		{
			eq.Add(item);
		}
	}

	public bool Remove(Object item)
	{
		try
		{
			remove(item);
		}
		catch (InventoryException ex)
		{
			EditorUtility.DisplayDialog("Inventory overload", ex.Message, "OK");
			return false;
		}
		return true;
	}
	private void remove(Object item)
	{
		if (--ObjectCount < 0)
		{
			throw new InventoryException("The inventory is empty !");
		}
		else
		{
			eq.Remove(item);
		}
	}

	//Properties
	public List<Object> Eq
	{
		get
		{
			return eq;
		}
	}
	public bool IsLimited
	{
		get
		{
			return sizeLimit != null;
		}
	}
}

