using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {

    public List<GameObject> all_waypoints;
    public GameObject thePlayer;
    private GameObject player_closest_waypoint;

    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        all_waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        all_waypoints.Add(thePlayer);
    }


    void Update()
    {
        if (FindClosestWaypoint(thePlayer) != player_closest_waypoint) // jak znalazl nowy najblizszy, to usuwa stary i ustawia nowy najblizszy waypoint
        {
            if (player_closest_waypoint != null)
            {
                player_closest_waypoint.GetComponent<OneWaypoint>().adjacent_waypoints.Remove(thePlayer);
            }
            player_closest_waypoint = FindClosestWaypoint(thePlayer);
            Debug.Log("Najblizej gracza: " + player_closest_waypoint.name);

            player_closest_waypoint.GetComponent<OneWaypoint>().adjacent_waypoints.Add(thePlayer);
        }
    }

    public GameObject FindClosestWaypoint(GameObject source)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Waypoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = source.transform.position;
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
