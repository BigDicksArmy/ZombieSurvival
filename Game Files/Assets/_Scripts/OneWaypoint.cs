using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWaypoint : MonoBehaviour {

    public List<GameObject> adjacent_waypoints;
    public List<float> adjacent_waypoints_cost;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < adjacent_waypoints.Count; ++i) // wypelnij koszty drogi tego waypointa do kazdego sasiadujacego
        {
            adjacent_waypoints_cost.Add((transform.position - adjacent_waypoints[i].transform.position).magnitude);
        }
    }

    void Update()
    {
        if (adjacent_waypoints.Contains(player)) // jesli sasiaduje z player, to dodaj jego odleglosc
        {
            adjacent_waypoints_cost.Add((transform.position - player.transform.position).magnitude);
        }

        while (adjacent_waypoints.Count < adjacent_waypoints_cost.Count) // jesli player byl i zniknal, to ten warunek bedzie spelniony trzeba playera odleglosc ZLIKWIDOWAC
            adjacent_waypoints_cost.RemoveAt(adjacent_waypoints.Count - 1);
    }
}
