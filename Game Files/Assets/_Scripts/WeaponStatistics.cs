using System;
using System.Reflection;

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
	public float BulletSpeed;
	public float FireRate;
	public float ReloadSpeed;
	public float BulletSpread;
	public FireMode firemode;
	public bool IsAutomatic;
}
