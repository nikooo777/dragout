using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISON");
        other.gameObject.GetComponent<PassengerScript>().DestroyPassenger();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COLLISON ENTER");
    }

}
