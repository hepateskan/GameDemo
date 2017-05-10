using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float verticalSpeed;
    public float amptitude; 

    public Vector2 tempPos;
	/*
	 * parent
     * transform.position += New Vector2(Input.GetAxis("Horizontal") * 
     * Time.smoothDeltaTime * horizontalSpeed, 0f);
     * 
     * 
     * child
     * transform.position += New Vector2(0f, Input.GetAxis("Verical") * 
     * Time.smoothDeltaTime * verticalSpeed);
     */

	void Start(){
        tempPos = transform.position;

    }
    void Update(){


        //tempPos.x += horizontalSpeed * Time.deltaTime;
        // tempPos.y = Mathf.Sin(Time.realtimeSinceStartup * (verticalSpeed *Time.deltaTime)) * amptitude;

        // tempPos.y += verticalSpeed * Time.smoothDeltaTime;
        //tempPos.x = 0f;


        //transform.position = tempPos;
        // transform.position += new Vector3(0f, (Input.GetAxis("Vertical") * Time.smoothDeltaTime * verticalSpeed),0);
        if (Input.GetAxis("Cancel") != 0)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
        transform.localPosition += new Vector3(0f, Input.GetAxis("Vertical") * Time.smoothDeltaTime * verticalSpeed, 0f);
    }





}
