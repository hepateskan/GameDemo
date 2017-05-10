using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakingDevice : PowerUp
{
    public static GameObject playerObject;
    public Sprite defaultSprite, cloakedSprite;
    public float timeCloaked = 5;

    // Use this for initialization
    void Start ()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject = playerObject.GetComponentInChildren<Rotation>().gameObject;
        }
        this.enabled = false;
    }

    protected override void PickUp()
    {
        playerObject.AddComponent<Timer>();
        ManageCloaking tempReference = playerObject.AddComponent<ManageCloaking>();
        tempReference.defaultSprite = defaultSprite;
        playerObject.GetComponent<SpriteRenderer>().sprite = cloakedSprite;
        base.PickUp();
    }
}
