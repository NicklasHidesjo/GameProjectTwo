using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The is the states player can be in, and the state the player is in.
//The player is also primarly updated via this script, hence a refrence to each playerScript is stored here.
//The state is also changed by calling SetState();

public class PlayerState : MonoBehaviour
{
    public enum playerStates
    {
        Stoped,
        TransformToDracula,

        DraculaDefault,
        DraculaAirborne,
        DraculaRunning,
        DraculaCrouching,

        DraculaDragBody,
        DraculaHideing,
        DraculaHidden,
        DraculaSucking,
        
        TransformToBat,
        BatDefault
    }

    [SerializeField] playerStates playerState;
    public playerStates CurrentState => playerState;
    private PlayerManager playerManeger;
    private DraculaMovement draculaMovement;
    private BatMovement batMovement;

    public void SetScipts(PlayerManager playerManeger, DraculaMovement draculaMovement, BatMovement batMovement)
    {
        this.playerManeger = playerManeger;
        this.draculaMovement = draculaMovement;
        this.batMovement = batMovement;
    }

    public playerStates GetCurrentState()
    {
        return playerState;
    }

    public void SetState(playerStates newState)
    {
        playerState = newState;
    }

    public void UpdateByState()
    {
//        Debug.Log("State : " + CurrentState);

        switch (playerState)
        {
            case playerStates.Stoped:
                {
                    Debug.Log("Disabled");
                    break;
                }
            case playerStates.TransformToDracula:
                {
                    playerManeger.ActivateDracula();
                    break;
                }
            case playerStates.DraculaDefault:
                {
                    draculaMovement.Move();
                    break;
                }
            case playerStates.DraculaAirborne:
                {
                    draculaMovement.Move();
                    break;
                }
            case playerStates.DraculaRunning:
                {
                    draculaMovement.Move();
                    break;
                }

            case playerStates.DraculaCrouching:
                {
                    draculaMovement.Move();
                    break;
                }
            case playerStates.DraculaHideing:
                {
                    break;
                }
            case playerStates.DraculaHidden:
                {
                    break;
                }
            case playerStates.DraculaSucking:
                {
                    break;
                }
            case playerStates.TransformToBat:
                {
                    playerManeger.ActivateBat();
                    break;
                }
            case playerStates.BatDefault:
                {
                    batMovement.Move();
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

