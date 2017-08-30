using System.Collections;
using UnityEngine;

public delegate void WeaponAudio(AudioClip clip);
public class WeaponController : MonoBehaviour
{
	public static WeaponAudio _Shot;
	public static WeaponAudio _Reload;
	public AudioClip Shooting;
	public AudioClip Reloading;

	public GameObject bulletSpawn;
	public WeaponStats stats;
	public float triggerPullTime = 0.1f;

	private EquipmentController equipment;
	private Transform weaponPlace;
	private Transform spawn;
	private Camera mainCamera;
	private float timeToFire = 0f;

	private void Awake()
	{
		mainCamera = Camera.main;
		weaponPlace = GetComponent<Transform>();
		equipment = transform.parent.GetComponent<EquipmentController>();
		spawn = weaponPlace.Find("BulletSpawn");
	}

	void Update()
	{
		switch (stats.firemode)
		{
			case FireMode.Single:
				SingleShot();
				break;
			case FireMode.Automatic:
				AutoShot();
				break;
			default:
				Debug.Log("Fire mode not choosen - not good");
				break;
		}
		if (Input.GetKeyDown(KeyCode.R) && stats.BulletsLeft >= 0 && stats.BulletsLeft <= stats.MagazineSize)
		{
			StartCoroutine(Reload(stats.ReloadSpeed));
		}
		if (Input.GetKeyDown(KeyCode.V) && stats.IsAutomatic)
		{
			//If AutoFire was on then switch to singleFire else switch to autoFire
			stats.firemode = stats.firemode == FireMode.Automatic ? stats.firemode = FireMode.Single : stats.firemode = FireMode.Automatic;
		}
	}

	#region Shooting Functions
	private void Shot()
	{
		Vector2 shotDir = CalculateDirection();
		Debug.Log(shotDir.ToString());
		RaycastHit2D raycastInfo = Physics2D.Raycast(spawn.position, shotDir);
		_Shot?.Invoke(Shooting);
		stats.BulletsLeft--;
	}

	private Vector2 CalculateDirection() //decided not to mess with the dot product of vectors
	{
		Vector2 symmetryLine = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float Bounduary = symmetryLine.magnitude * Mathf.Tan(Mathf.Deg2Rad * stats.BulletSpread / 2);
		float yCoord = UnityEngine.Random.Range(-Bounduary, Bounduary);
		Vector2 direction = new Vector2(symmetryLine.x, symmetryLine.y + yCoord);
		return direction;
	}
	IEnumerator Reload(float time)
	{
		if (stats.MagazineCount > 0 && stats.BulletsLeft < stats.MagazineSize)
		{
			_Reload?.Invoke(Reloading);
			yield return new WaitForSeconds(time);
			stats.BulletsLeft = stats.MagazineSize;
			stats.MagazineCount--;
		}
	}
	void SingleShot()
	{
		if (Input.GetButtonDown("Fire1") && stats.BulletsLeft > 0 && Time.time > timeToFire)
		{
			timeToFire = Time.time + triggerPullTime;
			Shot();
		}
	}
	void AutoShot()
	{
		if (Input.GetButton("Fire1") && stats.BulletsLeft > 0 && Time.time > timeToFire)
		{
			timeToFire = Time.time + 1 / stats.FireRate;
			Shot();
		}
	}
	#endregion
}
