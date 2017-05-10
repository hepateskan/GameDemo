using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour {

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;


    public GameObject player;


	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        

		
	}
}
