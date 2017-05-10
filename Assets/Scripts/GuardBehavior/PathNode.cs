using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    GuardBehavior script;

    void OnTriggerStay2D(Collider2D unit)
    {
        if (unit.tag == "Guard")
        {
            script = unit.gameObject.GetComponentInParent<GuardBehavior>();
            string curState = script.getState();

            if (curState == "Patrol")
            {                
                if (script.curTarget() == this.transform)
                {
                    //Debug.Log("Collided with " + unit.tag);
                    unit.gameObject.GetComponentInParent<GuardBehavior>().nextPathNode();
                }
            }
        }    
    }
}
