using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingUp : MonoBehaviour {

    private CharacterController thePlayer;
    //private NormalZombie theZombie;

    //private List<GameObject> normal_zombies;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<CharacterController>();
        //theZombie = FindObjectOfType<NormalZombie>();
        //normal_zombies.AddRange(GameObject.FindGameObjectsWithTag("Zombie"));
    }
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player") // zombie tez tu musza byc, moze wywalic warunek calkiem
        {
            thePlayer.canGoUp = true;
        }
        else if (other.tag == "Zombie")
        {
            other.GetComponent<NormalZombie>().canGoUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player") // zombie tez tu musza byc, moze wywalic warunek calkiem
        {
            thePlayer.canGoUp = false;
        }
        else if (other.tag == "Zombie")
        {
            other.GetComponent<NormalZombie>().canGoUp = false;
        }
    }
}
