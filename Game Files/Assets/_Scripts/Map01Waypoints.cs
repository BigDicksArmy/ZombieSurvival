using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map01Waypoints : MonoBehaviour {

    public List<GameObject> waypoints;
    public GameObject thePlayer;

    private GameObject player_closest_waypoint;
    private Waypoints player_closest_waypoint_script;


    // Use this for initialization
    void Start () {
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("WayPoints"));
    }
	
	// Update is called once per frame
	void Update () {

        if (FindClosestWaypoint() != player_closest_waypoint)
        {
            if (player_closest_waypoint_script != null)
            {
                player_closest_waypoint_script.adjacent_waypoints.Remove(thePlayer);
            }
            player_closest_waypoint = FindClosestWaypoint();
            Debug.Log("Najblizej gracza: " + player_closest_waypoint.name);

            player_closest_waypoint_script = player_closest_waypoint.GetComponent<Waypoints>();
            player_closest_waypoint_script.adjacent_waypoints.Add(thePlayer);
        }

         if (Input.GetKey(KeyCode.Return))
                player_closest_waypoint_script.adjacent_waypoints.Remove(thePlayer);
    }

    public GameObject FindClosestWaypoint()
    {
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("WayPoints");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = thePlayer.transform.position;
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
