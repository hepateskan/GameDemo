using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{

    float timeRemaining;
    bool finishedTiming;

	// Use this for initialization
	void Awake ()
    {
        finishedTiming = true;
        timeRemaining = 0f;
        enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            finishedTiming = true;
            enabled = false;
        }
	}

    public void setTimer(float startingTime)
    {
        timeRemaining = startingTime;
        finishedTiming = false;
        enabled = true;
    }

    public bool checkTimer()
    {
        return finishedTiming;
    }
}
