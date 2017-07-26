using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : MonoBehaviour {

    private CharacterController thePlayer;
    public float speed;
    private Rigidbody2D rb;

    public GameObject closest_waypoint;
    private WaypointManager waypoint_manager;

    public bool canGoUp;

    private float vertical_velocity;
    private float save_gravity;

    List<OneWaypoint> OPEN;
    List<OneWaypoint> CLOSE;

    public WaypointManager all_waypoints;

    void Start() {

        OPEN = new List<OneWaypoint>();
        CLOSE = new List<OneWaypoint>();

        rb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<CharacterController>();

        save_gravity = rb.gravityScale;

        closest_waypoint = FindClosestWaypoint();

        Pathfinding();
    }

    void Update() {
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, closest_waypoint.transform.position, speed * Time.deltaTime);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            Debug.Log("Odgryzlem Ci kurwa ryj");
    }

    public GameObject FindClosestWaypoint()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    
    public void Pathfinding()
    {
        OPEN.Add(closest_waypoint.GetComponent<OneWaypoint>());
        OPEN[0].f = 0;

        while(OPEN.Count != 0) // 3
        {
            OneWaypoint q = new OneWaypoint();
            q.f = Mathf.Infinity; 
            for (int i = 0; i < OPEN.Count; ++i) // a)
            {
                if (OPEN[i].f < q.f)
                    q = OPEN[i];
            }
            OPEN.Remove(q); // b)
            
            for (int j = 0; j < q.adjacent_waypoints.Count; ++j) // d)
            {
                if (q.adjacent_waypoints[j] == thePlayer) // i)
                {
                    break;
                }
                //q.adjacent_waypoints[j]
            }
        }
    } 
}




/* to bylo wczesniej w update
if (thePlayer.transform.position.x > rb.transform.position.x)
{
    rb.velocity = new Vector2(speed, 0);
}
else
{
    rb.velocity = new Vector2(-speed, 0);
}

if (canGoUp && thePlayer.transform.position.y > rb.transform.position.y)
{
    rb.gravityScale = 0f;

    vertical_velocity = speed / 2;
    rb.velocity = new Vector2(rb.velocity.x, vertical_velocity);
}
else if (canGoUp && thePlayer.transform.position.y < rb.transform.position.y)
{
    rb.gravityScale = 0f;

    vertical_velocity = speed / 2;
    rb.velocity = new Vector2(rb.velocity.x, -vertical_velocity); //rb.velocity.x,
}

if (!canGoUp || (thePlayer.transform.position.y <= rb.transform.position.y - 0.05 && thePlayer.transform.position.y >= rb.transform.position.y + 0.05))
{
    rb.gravityScale = save_gravity;
}
*/
