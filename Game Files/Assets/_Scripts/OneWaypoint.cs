using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWaypoint : MonoBehaviour {

    public List<GameObject> adjacent_waypoints;
    public List<float> adjacent_waypoints_cost;

    public float cost_to_player;

    public float dist;
    public GameObject prev;


    private GameObject player;

    void Start()
    {
        cost_to_player = Mathf.Infinity;
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < adjacent_waypoints.Count; ++i) // wypelnij koszty drogi tego waypointa do kazdego sasiadujacego
        {
            adjacent_waypoints_cost.Add((transform.position - adjacent_waypoints[i].transform.position).magnitude);
        }
    }

    void Update()
    {
        //(transform.position - player.transform.position).magnitude;
        
        if (adjacent_waypoints.Contains(player)) // jesli sasiaduje z player, to dodaj jego odleglosc
        {
            if (adjacent_waypoints.Count > adjacent_waypoints_cost.Count)
            {
                adjacent_waypoints_cost.Add((transform.position - player.transform.position).magnitude);
            }
            else if (adjacent_waypoints.Count == adjacent_waypoints_cost.Count)
            {
                adjacent_waypoints_cost[adjacent_waypoints_cost.Count - 1] = (transform.position - player.transform.position).magnitude;
            }
        }

        while (adjacent_waypoints.Count < adjacent_waypoints_cost.Count) // jesli player byl i zniknal, to ten warunek bedzie spelniony trzeba playera odleglosc ZLIKWIDOWAC
            adjacent_waypoints_cost.RemoveAt(adjacent_waypoints_cost.Count - 1);
    }
}
