using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum playerStates { Stoped, Movement, Hidden, Bat }
    private playerStates playerState;
    private Movement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerState = playerStates.Movement;
        }
        UpdateByState();
    }
    public void SetState(playerStates newState)
    {
        playerState = newState;
    }
    public playerStates GetCurrentState()
    {
        return playerState;
    }
    void UpdateByState()
    {
        switch (playerState)
        {
            case playerStates.Stoped:
                {
                    movement.UpdateCharacter();
                    Debug.Log("Disabled");
                    break;
                }
            case playerStates.Movement:
                {
                    movement.MoveCharacter();
                    break;
                }
            case playerStates.Hidden:
                {
                    break;
                }
            case playerStates.Bat:
                {
                    break;
                }
            default:
                {
                    Debug.Log("Non existent state");
                    break;
                }
        }
    }
}

