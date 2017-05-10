using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuBehavior : MonoBehaviour
{
    public float guiPlacementY1;
    public float guiPlacementY2;
    public float guiPlacementY3;
    public float guiPlacementY4;


    public float guiPlacementX1;
    public float guiPlacementX2;
    public float guiPlacementX3;
    public float guiPlacementX4;

    void OnGUI()
    {

        if (GUI.Button(new Rect(Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .3f, Screen.height * .05f), "NEW GAME"))
        {
            print("new game clicked");

        }

        if (GUI.Button(new Rect(Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .3f, Screen.height * .05f), "RESUME"))
        {
            print("RESUME clicked ");

        }

        if (GUI.Button(new Rect(Screen.width * guiPlacementX3, Screen.height * guiPlacementY3, Screen.width * .3f, Screen.height * .05f), "SETTINGS"))
        {
            print("settings clicked");

        }

        if (GUI.Button(new Rect(Screen.width * guiPlacementX4, Screen.height * guiPlacementY4, Screen.width * .3f, Screen.height * .05f), "QUIT"))
        {
            print("quit clicked");
        }


    }
}
