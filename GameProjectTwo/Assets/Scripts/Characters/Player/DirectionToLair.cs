using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionToLair : MonoBehaviour
{
    public SpriteRenderer lairFinder;
    Transform playerPos;
    Transform target;
    PlayerState playerState;
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        if (!playerManager)
        {
            playerManager = PlayerManager.instance;
        }
        playerPos = playerManager.GetPlayerPoint();
        target = GameObject.FindGameObjectWithTag("Lair").transform;

        // lairFinder = gameObject.GetComponent<SpriteRenderer>();
        // playerState = transform.parent.GetComponent<PlayerState>();

        /*
        if (!playerState)
        {
            playerState = PlayerManager.instance.playerState;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + new Vector3(0, -0.7f, 0);
        transform.forward = target.position - playerPos.position;
    }

}
