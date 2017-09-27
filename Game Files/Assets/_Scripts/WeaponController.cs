using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public AudioClip Shooting;
    public AudioClip Reloading;
    public Text Ammunition;
    // public GameObject BulletSpawn;
    public WeaponStats Stats;
    public float TriggerPullTime = 0.1f;
    public LayerMask DontHit;

    private AudioSource speakerShot;
    private AudioSource speakerReload;
    private EquipmentController equipment;
    private Transform weaponPlace;
    private Transform spawn;
    private Camera mainCamera;
    private float timeToFire = 0f;

    private void Awake()
    {
        weaponPlace = GetComponent<Transform>();
        equipment = transform.parent.GetComponent<EquipmentController>();
        speakerShot = transform.parent.GetComponent<AudioSource>();
        speakerReload = GetComponent<AudioSource>();
        spawn = weaponPlace.Find("BulletSpawn");
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (!speakerReload.isPlaying) //Shoot only if player stopped reloading
        {
            switch (Stats.firemode)
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

        }
        if (Input.GetKeyDown(KeyCode.R) && Stats.BulletsLeft >= 0 && Stats.BulletsLeft <= Stats.MagazineSize)
        {
            if (!speakerReload.isPlaying) //Reload only if player stopped reloading
            {
                StartCoroutine(Reload());
            }
        }
        if (Input.GetKeyDown(KeyCode.V) && Stats.IsAutomatic)
        {
            //If AutoFire was on then switch to singleFire else switch to autoFire
            Stats.firemode = Stats.firemode == FireMode.Automatic ? Stats.firemode = FireMode.Single : Stats.firemode = FireMode.Automatic;
        }

        Ammunition.text = Stats.BulletsLeft + " / " + Stats.MagazineCount * Stats.MagazineSize;
    }

    #region Shooting Functions

    private void Shot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = CalculateDirection(mousePosition - (Vector2)spawn.position, Stats.BulletSpread);
        RaycastHit2D raycastInfo = Physics2D.Raycast(spawn.position, direction, 100f, ~DontHit);

       //Debug.DrawLine(spawn.position, direction * 100, Color.yellow);

        if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Zombie"))
        {
            NormalZombie hitZombie = raycastInfo.collider.GetComponentInParent<NormalZombie>();
            hitZombie.DamageZombie(Stats.Damage);
            Debug.Log("Zombie hit:" + hitZombie.name + ", HP: " + hitZombie.CurrentHealth);
        }

        Stats.BulletsLeft--;

        speakerShot.clip = Shooting;
        speakerShot.Play();
    }
    private Vector2 CalculateDirection(Vector2 shotDirection, float BulletSpread)
    {
        float Bounduary = shotDirection.magnitude * Mathf.Tan(Mathf.Deg2Rad * BulletSpread / 2);
        float yCoord = UnityEngine.Random.Range(-Bounduary, Bounduary);
        Vector2 direction = new Vector2(shotDirection.x, shotDirection.y + yCoord);
        return direction;
    }

    private IEnumerator Reload()
    {
        if (Stats.MagazineCount > 0 && Stats.BulletsLeft < Stats.MagazineSize)
        {
            speakerReload.clip = Reloading;
            speakerReload.Play();
            yield return new WaitForSeconds(Reloading.length);
            Stats.BulletsLeft = Stats.MagazineSize;
            Stats.MagazineCount--;
        }
    }

    private void SingleShot()
    {
        if (Input.GetButtonDown("Fire1") && Stats.BulletsLeft > 0 && Time.time > timeToFire)
        {
            timeToFire = Time.time + TriggerPullTime;
            Shot();
        }
    }

    private void AutoShot()
    {
        if (Input.GetButton("Fire1") && Stats.BulletsLeft > 0 && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / Stats.FireRate;
            Shot();
        }
    }
    #endregion
}
