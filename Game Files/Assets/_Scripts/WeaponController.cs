using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public AudioClip Shooting;
    public AudioClip Reloading;

    public GameObject bulletSpawn;
    public WeaponStats stats;
    public float triggerPullTime = 0.1f;

    private AudioSource Speaker;
    private EquipmentController equipment;
    private Transform weaponPlace;
    private Transform spawn;
    private Camera mainCamera;
    private float timeToFire = 0f;

    private void Awake()
    {
        weaponPlace = GetComponent<Transform>();
        equipment = transform.parent.GetComponent<EquipmentController>();
        Speaker = transform.parent.GetComponent<AudioSource>();
        spawn = weaponPlace.Find("BulletSpawn");
        mainCamera = Camera.main;
    }

    private void Update()
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
            StartCoroutine(Reload());
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
        stats.BulletsLeft--;

        Speaker.clip = Shooting;
        Speaker.Play();
    }

    private IEnumerator Reload()
    {
        if (stats.MagazineCount > 0 && stats.BulletsLeft < stats.MagazineSize)
        {
            Speaker.clip = Reloading;
            Speaker.Play();
            yield return new WaitForSeconds(Reloading.length);
            stats.BulletsLeft = stats.MagazineSize;
            stats.MagazineCount--;
        }
    }

    private Vector2 CalculateDirection()
    {
        Vector2 symmetryLine = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float Bounduary = symmetryLine.magnitude * Mathf.Tan(Mathf.Deg2Rad * stats.BulletSpread / 2);
        float yCoord = UnityEngine.Random.Range(-Bounduary, Bounduary);
        Vector2 direction = new Vector2(symmetryLine.x, symmetryLine.y + yCoord);
        return direction;
    }

    private void SingleShot()
    {
        if (Input.GetButtonDown("Fire1") && stats.BulletsLeft > 0 && Time.time > timeToFire)
        {
            timeToFire = Time.time + triggerPullTime;
            Shot();
        }
    }

    private void AutoShot()
    {
        if (Input.GetButton("Fire1") && stats.BulletsLeft > 0 && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / stats.FireRate;
            Shot();
        }
    }
    #endregion
}
