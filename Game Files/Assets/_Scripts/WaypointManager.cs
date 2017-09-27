using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    #region Singleton
    private static WaypointManager _instance;

    public static WaypointManager Instance
    {
        get
        { return _instance; }

        private set
        { _instance = value; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("More than one instance of a singleton detected");
            return;
        }
        _instance = this;
    }
    #endregion

    public List<GameObject> all_waypoints;
	public GameObject player_object;
	private GameObject player_closest_waypoint;

	void Start()
	{
		player_object = GameObject.FindGameObjectWithTag("Player");
		all_waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
		all_waypoints.Add(player_object);

		player_object.GetComponent<OneWaypoint>().adjacent_waypoints.Add(FindClosestWaypoint(player_object));

		player_closest_waypoint = FindClosestWaypoint(player_object);

		player_object.GetComponent<MovementController>().IsClosestWaypointChanged = true;
	}


	void Update()
	{
		if (player_object.GetComponent<MovementController>().IsClosestWaypointChanged == true)
		{
			Dijsktra();
			player_object.GetComponent<MovementController>().IsClosestWaypointChanged = false;
		}


		if (FindClosestWaypoint(player_object) != player_closest_waypoint) // jak znalazl nowy najblizszy, to usuwa stary i ustawia nowy najblizszy waypoint
		{
			if (player_closest_waypoint != null)
			{
				player_closest_waypoint.GetComponent<OneWaypoint>().adjacent_waypoints.Remove(player_object);
				player_object.GetComponent<OneWaypoint>().adjacent_waypoints.Remove(player_closest_waypoint);
			}
			player_closest_waypoint = FindClosestWaypoint(player_object);
			Debug.Log("Najblizej gracza: " + player_closest_waypoint.name);

			player_closest_waypoint.GetComponent<OneWaypoint>().adjacent_waypoints.Add(player_object);
			player_object.GetComponent<OneWaypoint>().adjacent_waypoints.Add(player_closest_waypoint);

			player_object.GetComponent<MovementController>().IsClosestWaypointChanged = true;
		}
	}

	public GameObject FindClosestWaypoint(GameObject source) // moze zrobic druga funkcje, ktora szuka tylko po adjacent do tej, to gracza nie wywali
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

	public GameObject Find_Closest_Adjacent_Waypoint(GameObject source)
	{
		GameObject closest = null;
		float distance = Mathf.Infinity;
		for (int i = 0; i < source.GetComponent<OneWaypoint>().adjacent_waypoints.Count; ++i)
		{
			Vector3 diff = source.transform.position - source.GetComponent<OneWaypoint>().adjacent_waypoints[i].transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = source.GetComponent<OneWaypoint>().adjacent_waypoints[i];
				distance = curDistance;
			}
		}
		return closest;
	}

	public List<GameObject> Q;
	public GameObject u;

	void Dijsktra()
	{
		Debug.Log("Uruchamiam dijsktre:");
		for (int i = 0; i < all_waypoints.Count; ++i)
		{
			all_waypoints[i].GetComponent<OneWaypoint>().dist = Mathf.Infinity;
			all_waypoints[i].GetComponent<OneWaypoint>().prev = null;
		}

		player_object.GetComponent<OneWaypoint>().dist = 0; // jesli chce zmienic cel dijsktry, to wystarczy tutaj  // distance from source to source

		Q.AddRange(all_waypoints);
		while (Q.Count > 0)
		{
			u = Q[0];
			for (int i = 0; i < Q.Count; ++i)
			{
				if (u.GetComponent<OneWaypoint>().dist > Q[i].GetComponent<OneWaypoint>().dist)
					u = Q[i];
			}
			Q.Remove(u);

			float alt;
			for (int i = 0; i < u.GetComponent<OneWaypoint>().adjacent_waypoints.Count; ++i)
			{
				if (Q.Contains(u.GetComponent<OneWaypoint>().adjacent_waypoints[i]))
				{
					alt = u.GetComponent<OneWaypoint>().dist + (u.transform.position - u.GetComponent<OneWaypoint>().adjacent_waypoints[i].transform.position).magnitude;

					if (alt < u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist)
					{
						u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().dist = alt;
						u.GetComponent<OneWaypoint>().adjacent_waypoints[i].GetComponent<OneWaypoint>().prev = u;
					}
				}
			}
		}
	}
}
