using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotation : MonoBehaviour
{
    private static bool cloaked = false;
    public static Rotation onlyRotation;

    void Awake()
    {
        if (onlyRotation == null)
        {
            onlyRotation = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetButton("Left"))
        {
            if (Input.GetButton("Up"))
                transform.rotation = Quaternion.Euler(0, 0, 45);
            else if (Input.GetButton("Down"))
                transform.rotation = Quaternion.Euler(0, 0, 135);
            else
                transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetButton("Right"))
        {
            if (Input.GetButton("Up"))
                transform.rotation = Quaternion.Euler(0, 0, 315);
            else if (Input.GetButton("Down"))
                transform.rotation = Quaternion.Euler(0, 0, 225);
            else
                transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetButton("Up"))
        {
            if (Input.GetButton("Left"))
                transform.rotation = Quaternion.Euler(0, 0, 45);
            else if (Input.GetButton("Right"))
                transform.rotation = Quaternion.Euler(0, 0, 315);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetButton("Down"))
        {
            if (Input.GetButton("Left"))
                transform.rotation = Quaternion.Euler(0, 0, 135);
            else if (Input.GetButton("Right"))
                transform.rotation = Quaternion.Euler(0, 0, 225);
            else
                transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public static bool getCloakStatus()
    {
        return cloaked;
    }

    public static void toggleCloakStatus()
    {
        cloaked = !cloaked;
    }

    void OnCollisionEnter2D(Collision2D unit)
    {
        Debug.Log("Collided with " + unit.gameObject.tag);
        if (unit.gameObject.tag == "Guard")
        {
            Debug.Log("Restarting");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static float getCurrentRotation()
    {
        return onlyRotation.transform.eulerAngles.z;
    }
}
