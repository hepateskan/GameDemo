using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    public float horizontalSpeed;

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

	void Start()
	{
		tempPos = transform.position;

	}
	void Update()
	{

        //float tempSpeed = horizontalSpeed * Time.smoothDeltaTime;
        //tempPos.x += horizontalSpeed * Time.smoothDeltaTime;
        //tempPos.y = 0f;

        //transform.position += new Vector3((Input.GetAxis("Horizontal") * Time.smoothDeltaTime * horizontalSpeed), 0f, 0f);

        //transform.position = tempPos;
        transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * Time.smoothDeltaTime * horizontalSpeed, 0f, 0f);

	}

}

/*
 * 
 * float tempSpeed = speed * Time.smoothDeltaTime;
        transform.position += new Vector3(Input.GetAxis("Horizontal") * tempSpeed, Input.GetAxis("Vertical") * tempSpeed, 0);
 * 
 */
