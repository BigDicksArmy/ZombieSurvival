using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : MonoBehaviour {

    private CharacterController thePlayer;
    public float speed;
    private Rigidbody2D rb;

    public GameObject closest_waypoint;

    public bool canGoUp;

    private float vertical_velocity;
    private float save_gravity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<CharacterController>();

        save_gravity = rb.gravityScale;

        closest_waypoint = FindClosestWaypoint();
    }

    // Update is called once per frame
    void Update() {
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, closest_waypoint.transform.position, speed * Time.deltaTime);
        /*
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
            
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            Debug.Log("Odgryzlem Ci kurwa ryj");
    }

    public GameObject FindClosestWaypoint()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("WayPoints");
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
}
