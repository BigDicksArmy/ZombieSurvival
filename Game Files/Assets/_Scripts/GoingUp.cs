using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingUp : MonoBehaviour {

    private CharacterController thePlayer;
    private NormalZombie theZombie;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<CharacterController>();
        theZombie = FindObjectOfType<NormalZombie>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player") // zombie tez tu musza byc, moze wywalic warunek calkiem
        {
            thePlayer.canGoUp = true;
        }
        else if (other.tag == "Enemy")
        {
            theZombie.canGoUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player") // zombie tez tu musza byc, moze wywalic warunek calkiem
        {
            thePlayer.canGoUp = false;
        }
        else if (other.tag == "Enemy")
        {
            theZombie.canGoUp = false;
        }
    }
}
