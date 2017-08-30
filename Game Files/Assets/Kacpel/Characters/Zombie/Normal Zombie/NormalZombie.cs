using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    zrobic, ze jesli closest_waypoint to gracz, to olac wszystko i do niego isc
    
    wejscia do budynku zrobic innymi waypointami, zombie idzie tylko na poczatku do takiego,
    a potem nie widzi. Gracz w ogole nie widzi.

    w waypointach dodac czy waypoint jest otwarty, trzeba zrobic warunek w dijsktrze
*/

public class NormalZombie : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public GameObject player_object;
    //public Rigidbody2D player_rb;

    public GameObject closest_waypoint;
    private WaypointManager waypoint_manager;

    public bool canGoUp;

    private float vertical_velocity;
    private float save_gravity;

    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        //player_rb = player_object.GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();
        waypoint_manager = FindObjectOfType<WaypointManager>();

        save_gravity = rb.gravityScale;

        closest_waypoint = waypoint_manager.FindClosestWaypoint(gameObject);
        next_waypoint = closest_waypoint;
        check_skip_turn();
    }
    public GameObject next_waypoint;


    bool is_waypoint_nearby_x()
    {
        if ((next_waypoint.transform.position.x >= rb.transform.position.x - 0.1) && (next_waypoint.transform.position.x <= rb.transform.position.x + 0.1)) // jesli jest przy tym waypoincie (względem x)
        {
            return true;
        }
        return false;
    }

    bool is_waypoint_nearby_y()
    {
        if ((next_waypoint.transform.position.y >= rb.transform.position.y - 0.1) && (next_waypoint.transform.position.y <= rb.transform.position.y + 0.1)) // jesli jest przy tym waypoincie (względem x)
        {
            return true;
        }
        return false;
    }

    void check_skip_turn()
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

    bool on_player_floor()
    {
        if (next_waypoint != null && next_waypoint.GetComponent<OneWaypoint>().prev != null)
            if ((player_object.transform.position.y >= rb.transform.position.y - 0.1) && (player_object.transform.position.y <= rb.transform.position.y + 0.1))
                return true;
        return false;
    }

    //void Move()
    //{
    //    if (next_waypoint != null)
    //    {
    //        if (canGoUp == true)
    //        {
    //            rb.gravityScale = 0;
    //            if (is_waypoint_nearby_x() && next_waypoint.GetComponent<OneWaypoint>().on_stairs == true) // jesli jest w poblizu x od waypointa i jego waypoint jest na schodach
    //            {                                                               
    //                if (next_waypoint.transform.position.y > rb.transform.position.y) // idz w gore
    //                {
    //                    Debug.Log("gora");
    //                    rb.velocity = new Vector2(0, speed / 2 * Time.deltaTime);
    //                }
    //                else if (next_waypoint.transform.position.y < rb.transform.position.y) // idz w dol
    //                {
    //                    Debug.Log("dol");
    //                    rb.velocity = new Vector2(0, -speed / 2 * Time.deltaTime);
    //                }

    //                if (is_waypoint_nearby_y()) // jesli y jest mniej wiecej takie samo to przejdz do nastepnego waypointa
    //                {
    //                    Debug.Log("No");
    //                    next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
    //                    check_skip_turn();
    //                }
    //            }
    //            else
    //            {
    //                if (rb.transform.position.x < next_waypoint.transform.position.x) // jak nie na schodach i waypoint po prawej to idz w prawo
    //                {
    //                    Debug.Log("Prawodol");
    //                    rb.velocity = new Vector2(speed * Time.deltaTime, 0);
    //                }
    //                else if (rb.transform.position.x > next_waypoint.transform.position.x) // jak nie na schodach i waypoint po lewej to idz w lewo
    //                {
    //                    Debug.Log("Lewodol");
    //                    rb.velocity = new Vector2(-speed * Time.deltaTime, 0);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            rb.gravityScale = save_gravity;
    //            if (is_waypoint_nearby_x() == true) // jesli nie jest na schodach i jest w poblizu x waypointa, to znajdz inny
    //            {
    //                Debug.Log("YES");
    //                next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
    //                check_skip_turn();
    //            }
    //            else
    //            {
    //                if (rb.transform.position.x < next_waypoint.transform.position.x) // jak nie na schodach i waypoint po prawej to idz w prawo
    //                {
    //                    Debug.Log("Prawo");
    //                    rb.velocity = new Vector2(speed * Time.deltaTime, 0);
    //                }
    //                else if (rb.transform.position.x > next_waypoint.transform.position.x) // jak nie na schodach i waypoint po lewej to idz w lewo
    //                {
    //                    Debug.Log("Lewo");
    //                    rb.velocity = new Vector2(-speed * Time.deltaTime, 0);
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector2(0, rb.velocity.y);
    //    }
    //}

    void FixedUpdate()
    {
        //Move();
        if (next_waypoint != null)
        {
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, next_waypoint.transform.position, speed / 20 * Time.deltaTime);
            if (canGoUp == true)
            {
                rb.gravityScale = 0;
                if (next_waypoint.GetComponent<OneWaypoint>().on_stairs == true)
                {
                    if (is_waypoint_nearby_y())
                    {
                        next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                    }
                }
            }
            else
            {
                rb.gravityScale = save_gravity;
                if (is_waypoint_nearby_x() == true)
                {
                    next_waypoint = next_waypoint.GetComponent<OneWaypoint>().prev;
                }
            }

            if (player_object.GetComponent<CharacterController>().changed_closest_waypoint == true)
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
            Debug.Log("Odgryzlem Ci  ryj");
        if (other.tag == "Zombie")
        {
            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), other);
        }
    }
}