using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private ArmRotation arm;
	// Use this for initialization
	private void Awake()
	{
		arm = FindObjectOfType<ArmRotation>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
