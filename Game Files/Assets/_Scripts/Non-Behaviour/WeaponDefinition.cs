using UnityEngine;

public class Weapon
{
	public GameObject Firearm;
	public bool IsCollected;

	public Weapon(GameObject firearm, bool isCollected)
	{
		Firearm = firearm;
		IsCollected = isCollected;
	}
}

public enum FireMode
{
	Single = 1, Automatic
}

[System.Serializable]
public struct WeaponStats
{
	public uint BulletsLeft;
	public uint MagazineSize;
	public uint MagazineCount;
	public float FireRate;
	public float ReloadSpeed;
	public float BulletSpread;
	public FireMode firemode;
	public bool IsAutomatic;
}
