using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : MonoBehaviour {

    private CharacterController thePlayer;
    public float speed;
    private Rigidbody2D rb;

    public GameObject player_object;
    public GameObject closest_waypoint;
    private WaypointManager waypoint_manager;


    public bool canGoUp;

    private float vertical_velocity;
    private float save_gravity;

    // public List<OneWaypoint> OPEN;
    // public List<OneWaypoint> CLOSE;

    void Start() {
        player_object = GameObject.FindGameObjectWithTag("Player");

        //OPEN = new List<OneWaypoint>();
        //CLOSE = new List<OneWaypoint>();

        rb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<CharacterController>();
        waypoint_manager = FindObjectOfType<WaypointManager>();

        save_gravity = rb.gravityScale;

        closest_waypoint = waypoint_manager.FindClosestWaypoint(gameObject);
        
    }

    void Update() {
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, closest_waypoint.transform.position, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Return))
            dijsktra();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            Debug.Log("Odgryzlem Ci kurwa ryj");
    }

    public List<GameObject> Q;
    public GameObject u;
    void dijsktra()
    {
        Debug.Log("Uruchamiam dijsktre:");
        for (int i = 0; i < waypoint_manager.all_waypoints.Count; ++i)
        {
            waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().dist = Mathf.Infinity;
            waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().prev = null;
        }
        closest_waypoint.GetComponent<OneWaypoint>().dist = 0; // distance from source to source

        //for (int i = 0; i < waypoint_manager.all_waypoints.Count; ++i)
        //{
        //    if (waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().dist == 0)
        //        Debug.Log("WTF");
        //}

        Q.AddRange(waypoint_manager.all_waypoints);
        while (Q.Count > 0)
        {
            u = Q[0];
            for (int i = 0; i < Q.Count; ++i)
            {
                if (u.GetComponent<OneWaypoint>().dist > Q[i].GetComponent<OneWaypoint>().dist)
                    u = Q[i];
            }
            Debug.Log(u.GetComponent<OneWaypoint>().dist);
            Q.Remove(u);

            float alt;
            for (int i = 0; i < u.GetComponent<OneWaypoint>().adjacent_waypoints.Count; ++i) // nizej jeszcze 1 if z ?
            {
                if (Q.Contains(u.GetComponent<OneWaypoint>().adjacent_waypoints[i])) //??
                {
                    alt = u.GetComponent<OneWaypoint>().dist + u.GetComponent<OneWaypoint>().adjacent_waypoints_cost[i];  // z closest waypoint do u + z u do aktywnego z adjacent waypointow
                    Debug.Log(u.GetComponent<OneWaypoint>().dist + "  " + u.GetComponent<OneWaypoint>().adjacent_waypoints_cost[i]);
                    //Debug.Log(alt + "  " + u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist);

                    if (alt < u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist)
                    {
                        Debug.Log("Zmieniam wazny element");
                        u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist = alt;
                        u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().prev = u;
                    }
                }
            }

        }
    }

    /////////////////////////////// to moze zadzialac
    /*
    public List<GameObject> Q;
    public GameObject u;
    void dijsktra()
    {
        Debug.Log("Uruchamiam dijsktre");
        for (int i = 0; i < waypoint_manager.all_waypoints.Count; ++i)
        {
            //waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().dist = Mathf.Infinity;
            waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().prev = null;
        }
        //closest_waypoint.GetComponent<OneWaypoint>().cost_to_player = 0;
        Q.AddRange(waypoint_manager.all_waypoints);

        while (Q.Count > 0)
        {
            u = Q[0];
            for (int i = 0; i < Q.Count; ++i)
            {
                if (u.GetComponent<OneWaypoint>().cost_to_player < Q[i].GetComponent<OneWaypoint>().cost_to_player)
                    u = Q[i];
            }
            Q.Remove(u);

            float alt;
            for (int i = 0; i < u.GetComponent<OneWaypoint>().adjacent_waypoints.Count; ++i)
            {
                if (Q.Contains(u.GetComponent<OneWaypoint>().adjacent_waypoints[i])) //?
                {
                    alt = u.GetComponent<OneWaypoint>().cost_to_player + u.GetComponent<OneWaypoint>().adjacent_waypoints_cost[i];

                    if (alt < u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().cost_to_player) //nigdy sie nie wykonuje
                    {
                        Debug.Log("Zmienilem cos w prev dla i = " + i);
                        u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().cost_to_player = alt; //u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist = alt;
                        u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().prev = u;
                    }
                }
            }
        }
    }
    */
    ///////////////////////////////

    /*
    public List<GameObject> pq;
    void dijsktra()
    {
        pq.Add(closest_waypoint);

        while (pq.Count > 0)
        {
            
            for (int i = 0; i <pq.Count; ++i) // usuwamy najmniejsze z pq
            {
                if (pq[i].GetComponent<OneWaypoint>().cost_to_player < waypoint_manager.GetComponent<FakeWaypoint>().cost_to_player)
                    waypoint_manager.GetComponent<FakeWaypoint>().cost_to_player = pq[i].GetComponent<OneWaypoint>().cost_to_player;
            }
            for (int j = 0; j < )

        }

    }
    */
    /*
    //  Dijsktra
    public float[] prev;
    public float[] dist;
    public float[] selected;

    void dijsktra()
    {
        // source to transform.position
        // target to player_object.transform.position
        // cost to waypoint_manager.all_waypoints[j].GetComponent<OneWaypoint>().adjacent_waypoints_cost ?
        dist = new float[waypoint_manager.all_waypoints.Count];
        prev = new float[waypoint_manager.all_waypoints.Count];
        selected = new float[waypoint_manager.all_waypoints.Count];

        int start = -1;
        int target = -1;
        for (int i = 0; i < waypoint_manager.all_waypoints.Count; ++i) //znajdz indeks closest waypoint to start
        {
            if (waypoint_manager.all_waypoints[i] == closest_waypoint)
                start = i;
            if (waypoint_manager.all_waypoints[i] == player_object)
                target = i;
        }

        if (start == -1 || target == -1)
            Debug.Log("Blad 83 linijka normal zombie");

        for (int i = 0; i < selected.Length; ++i)
        {
            selected[i] = 0f;
            dist[i] = Mathf.Infinity;
            prev[i] = -1f;
        }

        selected[start] = 1;
        dist[start] = 0;
        int m;
        float min, d;
        while(selected[target] == 0)
        {
            min = Mathf.Infinity;
            m = 0;
            for (int j = 0; j < waypoint_manager.all_waypoints.Count; ++j)
            {
                int temp = -1;
                for (int k = 0; k < waypoint_manager.all_waypoints[start].GetComponent<OneWaypoint>().adjacent_waypoints_cost.Count; ++k)
                {
                    if (waypoint_manager.all_waypoints[start] == waypoint_manager.all_waypoints[start].GetComponent<OneWaypoint>().adjacent_waypoints[k])
                        temp = k;
                }
                

                d = dist[start] + waypoint_manager.all_waypoints[start].GetComponent<OneWaypoint>().adjacent_waypoints_cost[temp]; // odleglosc od startu do waypointa [j]

                if (d < dist[j] && selected[j] == 0)
                {
                    dist[j] = d;
                    prev[j] = start;
                }
                if (min > dist[j] && selected[j] == 0)
                {
                    min = dist[j];
                    m = j;
                }
            }
            start = m;
            selected[start] = 1;
        }
        start = target;
        Debug.Log("Odleglosc to: " + dist[target]);
        //Debug.Log("Droga: " + )
    }
    
    */

    /* A*
    public void Pathfinding()
    {
        OPEN.Add(closest_waypoint.GetComponent<OneWaypoint>());
        OPEN[0].f = 0;
        
        while(OPEN.Count != 0) // 3
        {
            OneWaypoint q = OPEN[0];
            for (int i = 0; i < OPEN.Count; ++i) // a)
            {
                if (OPEN[i].f < q.f)
                    q = OPEN[i];
            }
            OPEN.Remove(q); // b)  //tu moze byc blad


            for (int a = 0; a < waypoint_manager.all_waypoints.Count; ++a)
                //for (int b = 0; b < waypoint_manager.all_waypoints[a].GetComponent<OneWaypoint>().adjacent_waypoints.Count; ++b)
            {
                waypoint_manager.all_waypoints[a].GetComponent<OneWaypoint>().g = (waypoint_manager.all_waypoints[a].transform.position - gameObject.transform.position).magnitude;
                waypoint_manager.all_waypoints[a].GetComponent<OneWaypoint>().h = (thePlayer.transform.position - waypoint_manager.all_waypoints[a].transform.position).magnitude;
            }
                    
            
            for (int j = 0; j < q.adjacent_waypoints.Count; ++j) // d)
            {
                if (q.adjacent_waypoints[j] == thePlayer) // i)
                {
                    q.adjacent_waypoints[j].GetComponent<OneWaypoint>().g = q.g + (q.adjacent_waypoints[j].transform.position - q.transform.position).magnitude;
                    q.adjacent_waypoints[j].GetComponent<OneWaypoint>().h = (player_object.transform.position - q.adjacent_waypoints[j].transform.position).magnitude;
                    q.adjacent_waypoints[j].GetComponent<OneWaypoint>().f = q.adjacent_waypoints[j].GetComponent<OneWaypoint>().g + q.adjacent_waypoints[j].GetComponent<OneWaypoint>().h;

                    break; // stop search to chyba inaczej
                }

                //jeśli transform position jest takie same, tylko f mniejsze // ii)
                bool next = false;
                for (int k = 0; k < OPEN.Count; ++k)
                {
                    if (OPEN[k].transform.position == q.adjacent_waypoints[j].transform.position)
                        if (OPEN[k].f < q.adjacent_waypoints[j].GetComponent<OneWaypoint>().f)
                        {
                            next = true;
                            continue;
                        }
                }
                if (next == true)
                    continue;


                // iii)
                bool next2 = false;
                for (int h = 0; h < CLOSE.Count; ++h)
                {
                    if (CLOSE[h].transform.position == q.adjacent_waypoints[j].transform.position)
                        if (CLOSE[h].f < q.adjacent_waypoints[j].GetComponent<OneWaypoint>().f)
                        {
                            next2 = true;
                            continue;
                        }
                        else
                        {
                            OPEN.Add(CLOSE[h]);
                        }
                }
                if (next2 == true)
                    continue;
            }

            CLOSE.Add(q); // e)
        }
    } 
    */
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








    /*
/////////////////////////////// to moze zadzialac
public List<GameObject> Q;
void dijsktra()
{
    for (int i = 0; i < waypoint_manager.all_waypoints.Count; ++i)
    {
        waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().dist = Mathf.Infinity;
        waypoint_manager.all_waypoints[i].GetComponent<OneWaypoint>().prev = null;
    }
    closest_waypoint.GetComponent<OneWaypoint>().dist = 0;
    Q.AddRange(waypoint_manager.all_waypoints);
    while (Q.Count > 0)
    {
        GameObject u = Q[0];
        for (int i = 0; i < Q.Count; ++i)
        {
            if (u.GetComponent<OneWaypoint>().dist < Q[i].GetComponent<OneWaypoint>().dist)
                u = Q[i];
        }
        Q.Remove(u);


        float alt;
        for (int i = 0; i < u.GetComponent<OneWaypoint>().adjacent_waypoints.Count; ++i)
        {
            if (Q.Contains(u.GetComponent<OneWaypoint>().adjacent_waypoints[i])) //?
            {
                alt = u.GetComponent<OneWaypoint>().dist + u.GetComponent<OneWaypoint>().adjacent_waypoints_cost[i];

                if (alt < u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist)
                {
                    u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist = alt;
                    u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().prev = u;
                }
            }
        }
    }
}
///////////////////////////////*/