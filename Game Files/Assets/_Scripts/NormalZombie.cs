using UnityEngine;
using UnityEngine.UI;

/*
    zrobic, ze jesli closest_waypoint to gracz, to olac wszystko i do niego isc
    
    wejscia do budynku zrobic innymi waypointami, zombie idzie tylko na poczatku do takiego,
    a potem nie widzi. Gracz w ogole nie widzi.

    w waypointach dodac czy waypoint jest otwarty, trzeba zrobic warunek w dijsktrze
*/

public class NormalZombie : MonoBehaviour
{
    public Transform HealthBar;
    public float MaxHealth;
    public float CurrentHealth;
    public float Damage;
    public float Speed;

    private GameObject next_waypoint;
    private GameObject closest_waypoint;
    private Rigidbody2D rb;
    private WaypointManager waypoint_manager;
    private GameObject player_object;
    private float vertical_velocity;
    private float save_gravity;

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
            if ((next_waypoint.transform.position.x >= rb.transform.position.x - 0.1) && (next_waypoint.transform.position.x <= rb.transform.position.x + 0.1)) // jesli jest przy tym waypoincie (względem x)
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
            if ((next_waypoint.transform.position.y >= rb.transform.position.y - 0.1) && (next_waypoint.transform.position.y <= rb.transform.position.y + 0.1)) // jesli jest przy tym waypoincie (względem x)
            {
                return true;
            }
            return false;
        }
    }
    #endregion

    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.localScale = new Vector3(MaxHealth / 10f, 2, 0);

        player_object = GameObject.FindGameObjectWithTag("Player");
        //player_rb = player_object.GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();
        waypoint_manager = FindObjectOfType<WaypointManager>();

        closest_waypoint = waypoint_manager.FindClosestWaypoint(gameObject);
        next_waypoint = closest_waypoint;
        CheckSkipTurn();
    }

    public void DamageZombie(float Damage)
    {
        CurrentHealth -= Damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
        HealthBar.localScale = new Vector3(CurrentHealth / 10f, 2, 0);
    }

    void CheckSkipTurn()
    {
        if (next_waypoint != null && next_waypoint.GetComponent<OneWaypoint>().prev != null)
            if ((next_waypoint.GetComponent<OneWaypoint>().on_stairs == false))// || (next_waypoint.GetComponent<OneWaypoint>().on_stairs == true && next_waypoint.GetComponent<OneWaypoint>().prev.GetComponent<OneWaypoint>().on_stairs == false))
            {
                if ((next_waypoint.transform.position.x > rb.transform.position.x && next_waypoint.GetComponent<OneWaypoint>().prev.transform.position.x < rb.transform.position.x)
                 || (next_waypoint.transform.position.x < rb.transform.position.x && next_waypoint.GetComponent<OneWaypoint>().prev.transform.position.x > rb.transform.position.x))
                {
                    next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                }
            }
    }
#warning Useless OnPlayerFloor Function
    bool on_player_floor()
    {
        if (next_waypoint != null && next_waypoint.GetComponent<OneWaypoint>().prev != null)
            if ((player_object.transform.position.y >= rb.transform.position.y - 0.1) && (player_object.transform.position.y <= rb.transform.position.y + 0.1))
                return true;
        return false;
    }

    void FixedUpdate()
    {
        //Move();
        if (next_waypoint != null)
        {
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, next_waypoint.transform.position, Speed * Time.deltaTime);
            if (IsGravityOff)
            {
                // rb.gravityScale = 0;
                if (next_waypoint.GetComponent<OneWaypoint>().on_stairs == true)
                {
                    if (IsWaypointNearbyY)
                    {
                        next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                    }
                }
            }
            else
            {
                // rb.gravityScale = save_gravity;
                if (IsWaypointNearbyX == true)
                {
                    next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                }
            }

            if (player_object.GetComponent<MovementController>().IsClosestWaypointChanged == true)
            {
                next_waypoint = waypoint_manager.FindClosestWaypoint(gameObject);
                if (next_waypoint.GetComponent<OneWaypoint>().prev != null)
                {
                    if (player_object.transform.position.x < next_waypoint.transform.position.x && next_waypoint.transform.position.x > next_waypoint.GetComponent<OneWaypoint>().prev.transform.position.x)
                    {
                        next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                    }
                }
            }
        }
        else
        {
            next_waypoint = waypoint_manager.FindClosestWaypoint(gameObject);
            if (next_waypoint.GetComponent<OneWaypoint>().prev != null)
            {
                if (player_object.transform.position.x < next_waypoint.transform.position.x && next_waypoint.transform.position.x > next_waypoint.GetComponent<OneWaypoint>().prev.transform.position.x)
                {
                    next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            Debug.Log("Collided with player");
        if (other.tag == "Zombie")
        {
            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), other);
        }
    }
}