using System.Collections;
using UnityEngine;

/*
    zrobic, ze jesli closestWaypoint to gracz, to olac wszystko i do niego isc
    
    wejscia do budynku zrobic innymi waypointami, zombie idzie tylko na poczatku do takiego,
    a potem nie widzi. Gracz w ogole nie widzi.

    w waypointach dodac czy waypoint jest otwarty, trzeba zrobic warunek w dijsktrze
*/

public class NormalZombie : MonoBehaviour
{
    public Transform HealthBar;
    public CapsuleCollider2D AttackTrigger;

    private GameObject nextWaypoint;
    private GameObject closestWaypoint;
    private Rigidbody2D body;
    private GameObject playerObject;
    [SerializeField] private float currentHealth;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private float attackInterval;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;

    #region Properties
    public bool IsGravityOff
    {
        get
        {
            return GetComponent<MovementController>().IsGravityOff;
        }
    }

    bool IsWaypointNearbyX
    {
        get
        {
            if ((nextWaypoint.transform.position.x >= body.transform.position.x - 0.1) && (nextWaypoint.transform.position.x <= body.transform.position.x + 0.1)) // jesli jest przy tym waypoincie (względem x)
            {
                return true;
            }
            return false;
        }
    }

    bool IsWaypointNearbyY
    {
        get
        {
            if ((nextWaypoint.transform.position.y >= body.transform.position.y - 0.1) && (nextWaypoint.transform.position.y <= body.transform.position.y + 0.1)) // jesli jest przy tym waypoincie (względem x)
            {
                return true;
            }
            return false;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public float AttackInterval
    {
        get
        {
            return attackInterval;
        }

        set
        {
            attackInterval = value;
        }
    }

    public float AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }
    #endregion

    void OnEnable()
    {
        AttackTrigger.size = new Vector2(AttackRange, AttackTrigger.size.y);    //Scale the trigger 
        CurrentHealth = MaxHealth;
    }
    void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        closestWaypoint = WaypointManager.Instance.FindClosestWaypoint(gameObject);
    }
    void Start()
    {
        HealthBar.localScale = new Vector3(MaxHealth / 10f, 2, 0);
        nextWaypoint = closestWaypoint;
        CheckSkipTurn();
    }

    void FixedUpdate()
    {
        if (nextWaypoint != null)
        {
            body.transform.position = Vector2.MoveTowards(body.transform.position, nextWaypoint.transform.position, Speed * Time.deltaTime);
            if (IsGravityOff)
            {
                if (nextWaypoint.GetComponent<OneWaypoint>().on_stairs)
                {
                    if (IsWaypointNearbyY)
                    {
                        nextWaypoint = nextWaypoint.GetComponent<OneWaypoint>().prev;
                    }
                }
            }
            else
            {
                if (IsWaypointNearbyX)
                {
                    nextWaypoint = nextWaypoint.GetComponent<OneWaypoint>().prev;
                }
            }

            if (playerObject.GetComponent<MovementController>().IsClosestWaypointChanged)
            {
                nextWaypoint = WaypointManager.Instance.FindClosestWaypoint(gameObject);
                if (nextWaypoint.GetComponent<OneWaypoint>().prev != null)
                {
                    if (playerObject.transform.position.x < nextWaypoint.transform.position.x && nextWaypoint.transform.position.x > nextWaypoint.GetComponent<OneWaypoint>().prev.transform.position.x)
                    {
                        nextWaypoint = nextWaypoint.GetComponent<OneWaypoint>().prev;
                    }
                }
            }
        }
        else
        {
            nextWaypoint = WaypointManager.Instance.FindClosestWaypoint(gameObject);
            if (nextWaypoint.GetComponent<OneWaypoint>().prev != null)
            {
                if (playerObject.transform.position.x < nextWaypoint.transform.position.x && nextWaypoint.transform.position.x > nextWaypoint.GetComponent<OneWaypoint>().prev.transform.position.x)
                {
                    nextWaypoint = nextWaypoint.GetComponent<OneWaypoint>().prev;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlayerHB")
        {
            StartCoroutine(DamagePlayer(Damage, AttackInterval));
        }
        if (collider.tag == "Zombie")
        {
            //Ignore the collision with another zombie
            Physics2D.IgnoreCollision(body.GetComponent<Collider2D>(), collider);
        }
    }
    void CheckSkipTurn()
    {
        if (nextWaypoint != null && nextWaypoint.GetComponent<OneWaypoint>().prev != null)
            if ((nextWaypoint.GetComponent<OneWaypoint>().on_stairs == false))// || (nextWaypoint.GetComponent<OneWaypoint>().on_stairs == true && nextWaypoint.GetComponent<OneWaypoint>().prev.GetComponent<OneWaypoint>().on_stairs == false))
            {
                if ((nextWaypoint.transform.position.x > body.transform.position.x && nextWaypoint.GetComponent<OneWaypoint>().prev.transform.position.x < body.transform.position.x)
                 || (nextWaypoint.transform.position.x < body.transform.position.x && nextWaypoint.GetComponent<OneWaypoint>().prev.transform.position.x > body.transform.position.x))
                {
                    nextWaypoint = nextWaypoint.GetComponent<OneWaypoint>().prev;
                }
            }
    }

    public void DamageZombie(float Damage)
    {
        CurrentHealth -= Damage; //Damage the zombie
        if (CurrentHealth <= 0) //if no health left destroy the game object
        {
            Destroy(gameObject);
            WavesManager.Instance.remainingZombies--; //update the remaining zombies count in the waves manager
        }
        HealthBar.localScale = new Vector3(CurrentHealth / 10f, 2, 0); // update the zombie health bar
    }

    public IEnumerator DamagePlayer(float Damage, float AttackInterval)
    {
        yield return new WaitForSecondsRealtime(AttackInterval);
        PlayerController.Instance.DamagePlayer(Damage);
    }

}