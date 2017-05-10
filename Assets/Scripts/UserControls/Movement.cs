using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed = 1;
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis("Cancel") != 0)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
        float tempSpeed = speed * Time.smoothDeltaTime;
        transform.position += new Vector3(Input.GetAxis("Horizontal") * tempSpeed, Input.GetAxis("Vertical") * tempSpeed, 0);
	}
}
