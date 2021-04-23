using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionToLair : MonoBehaviour
{
    SpriteRenderer lairFinder;
    Transform target;
    PlayerState playerState;
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Awake()
    {
        lairFinder = gameObject.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Lair").transform;
        playerState = transform.parent.GetComponent<PlayerState>();
        playerManager = transform.parent.GetComponent<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
        Transform playerPos = playerManager.GetPlayerPoint();
        transform.position = playerPos.position;
            //get player.state bat or dracula, and set y position after that...-0.7 ca for draculas placeholder.
            transform.position = transform.position + new Vector3(0, -0.8f, 0);
        Vector3 targetPosition = new Vector3(target.transform.position.x, playerPos.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
        transform.Rotate(90, 0, 0);
        }
    }

}
