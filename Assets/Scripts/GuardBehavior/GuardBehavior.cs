using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardBehavior : MonoBehaviour
{
    public enum guardState
    {
        Patrol,
        Chase,
        Search,
        Look
    }

    private ConstantRotation rotater;

    public Transform target;
    
    Vector2[] path;
    int targetIndex;

    private guardState curState = guardState.Patrol;
    public float speed = 1.25f; // Guard's initial speed
    public float searchRadius;  // Guard's search radius after losing player.

    private bool alerted = false;   // if true, speed *2

    // Pathing helper for guard
    public Transform[] pathNodes = new Transform[4];
    private int pathNodeHelper = 0;

    // Chase and search state helpers
    private Transform thief;

    public Transform lastSeenPosTrans;
    private bool searchingRandPos = false;

    private float turntime = 0;

    // Use this for initialization
    void Start()
    {
        if (thief == null)
        {
            thief = GameObject.FindGameObjectWithTag("Player").transform;
        }
        rotater = gameObject.AddComponent<ConstantRotation>();
        rotater.parentGuard = this;
        StartCoroutine(RefreshPath());
    }

    public Transform curTarget()
    {
        return target;
    }

    public void setState(guardState state)
    {
        curState = state;
        rotater.enabled = false;
        switch (state)
        {
            case guardState.Patrol:
                target = pathNodes[pathNodeHelper]; // continue to last node
                break;
            case guardState.Chase:
                if (!alerted)
                {
                    alerted = true;
                    speed *= 1.5f;
                }
                target = thief;
                break;
            case guardState.Search:
                Debug.Log("Search state");
                target = lastSeenPosTrans;
                break;
            case guardState.Look:
                rotater.timeToRotate.setTimer(2f);
                rotater.enabled = true;
                break;
        }
    }

    public void reportPosition(Vector3 lastSeen)
    {
        lastSeenPosTrans.transform.position = lastSeen;
    }

    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

        while (true)
        {
            if (targetPositionOld != (Vector2)target.position && curState != guardState.Look)
            {
                targetPositionOld = (Vector2)target.position;

                path = Pathfinding.RequestPath(transform.position, target.position);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector3 currentWaypoint = path[0];
            while (true)
            {
                if ((Vector3)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                transform.up = currentWaypoint - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.smoothDeltaTime);
                yield return null;
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                //Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

    //IEnumerator look()
    //{
        //transform.Rotate(transform.forward, 9f * Time.smoothDeltaTime);
        //turntime += Time.smoothDeltaTime;
        /*
        if (turntime > 10)
        {
            StopCoroutine(look());
            StartCoroutine(RefreshPath());
            turntime = 0;
            target = pathNodes[pathNodeHelper];
        }*/
        //yield return null;
   // }

    //Determines the next node in the guards path and 
    public void nextPathNode()
    {
        if (pathNodeHelper == pathNodes.Length - 1)
        {
            pathNodeHelper = 0;
        }
        else
        {
            pathNodeHelper++;
        }

        //Debug.Log("Next node = " + pathNodeHelper);

        //StopCoroutine("FollowPath");

        target = pathNodes[pathNodeHelper];
        //Transitions to the look state
        setState(guardState.Look);
        //StopAllCoroutines();

        //StartCoroutine(look());

        //StartCoroutine(RefreshPath());
    }


    public string getState()
    {
        if (curState == guardState.Patrol)
        {
            return "Patrol";
        }
        else if (curState == guardState.Search)
        {
            return "Search";
        }
        else
        {
            return "Chase";
        }
    }

    public void OnTriggerEnter2D(Collider2D unit)
    {
        if (curState == guardState.Search)
        {
            string thisTag = unit.gameObject.tag;

            if(searchingRandPos && thisTag == "Search")
            {
                //Debug.Log("Guard reached random position, returning to patrol");
                setState(guardState.Patrol);
                searchingRandPos = false;
            }
            // Search state's search different area after last seen pos.
            else if (thisTag == "Search" || thisTag == "Guard" && !searchingRandPos)
            {
                //Debug.Log("Guard searching at new random position");
                lastSeenPosTrans.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
                target = lastSeenPosTrans;
                searchingRandPos = true;
            }
        }
    }
}
