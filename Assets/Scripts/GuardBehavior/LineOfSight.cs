using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineOfSight : MonoBehaviour
{
    private GameObject player;
    GameObject[] guards;
    public GameObject guardAttached;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        // alert attached guard
        guards = GameObject.FindGameObjectsWithTag("Guard");
    }

    private void Update()
    {
        transform.position = guardAttached.transform.position + (guardAttached.transform.up * 1.2f);
    }

    void OnTriggerEnter2D(Collider2D unit)
    {
        // Only raycast if the player has hit the collider
        if (unit.tag == "Player")
        {
            // Get the direction from guard to player
            Vector3 rayCastDir = (player.transform.position - guardAttached.transform.position);

            // Raycast towards the player's direction to see if there is any obstacles in the way.
            RaycastHit2D rayToUser =
                Physics2D.Raycast(guardAttached.transform.position, rayCastDir, 5f);

            // Draw the raycast in scene editor
            Debug.DrawRay(guardAttached.transform.position, rayCastDir, Color.green);

            // Debugs for now, check what the raycast collides with in the player direction 
            if (rayToUser.collider.tag == "Player" && !Rotation.getCloakStatus())
            {
                alertGuards();
            }
        }
    }

    void OnTriggerExit2D(Collider2D unit)
    {
        // Only raycast if the player has hit the collider
        if (unit.tag == "Player" && !Rotation.getCloakStatus())
        {            
            searchGuards(unit.transform.position);
        }
    }

    void searchGuards(Vector3 unit)
    {
        // Set all guards to search state
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].GetComponentInParent<GuardBehavior>().setState(GuardBehavior.guardState.Search);
            guards[i].GetComponentInParent<GuardBehavior>().reportPosition(unit);
        }
    }

    void alertGuards()
    {
        // Alert all guards
        for(int i = 0; i < guards.Length; i++)
        {
            guards[i].GetComponentInParent<GuardBehavior>().setState(GuardBehavior.guardState.Chase);
        }
    }
}
