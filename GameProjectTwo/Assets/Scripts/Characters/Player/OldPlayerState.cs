using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The is the states player can be in, and the state the player is in.
//The player is also primarly updated via this script, hence a refrence to each playerScript is stored here.
//The state is also changed by calling SetState();

public class OldPlayerState : MonoBehaviour
{
    [SerializeField] PlayerStates playerState;
    public PlayerStates CurrentState => playerState;
    private PlayerManager playerManeger;
    private DraculaMovement draculaMovement;
    private BatMovement batMovement;

    public void SetScipts(PlayerManager playerManeger, DraculaMovement draculaMovement, BatMovement batMovement)
    {
        this.playerManeger = playerManeger;
        this.draculaMovement = draculaMovement;
        this.batMovement = batMovement;
    }

    public PlayerStates GetCurrentState()
    {
        return playerState;
    }

    public void SetState(PlayerStates newState)
    {
        //Debug.Log("<color=red> SET STATE TO : </color>" + newState);
        playerState = newState;
    }

    public void UpdateByState()
    {
     //   Debug.Log("<color=yellow> in state : </color>" + CurrentState);

        switch (playerState)
        {
            case PlayerStates.Stoped:
                {
                    Debug.Log("Player Disabled");
                    break;
                }
            case PlayerStates.TransformToDracula:
                {
                    playerManeger.ActivateDracula();
                    break;
                }
            case PlayerStates.DraculaDefault:
                {
                    draculaMovement.Move();
                    break;
                }
            case PlayerStates.DraculaAirborne:
                {
                    draculaMovement.Move();
                    break;
                }
            case PlayerStates.DraculaRunning:
                {
                    draculaMovement.Move();
                    break;
                }

            case PlayerStates.DraculaCrouching:
                {
                    draculaMovement.Move();
                    break;
                }
            case PlayerStates.DraculaDragBody:
                {
                    draculaMovement.DragBody();
                    break;
                }

            case PlayerStates.DraculaHideing:
                {
                    break;
                }
            case PlayerStates.DraculaHidden:
                {
                    break;
                }
            case PlayerStates.DraculaSucking:
                {
                    break;
                }
            case PlayerStates.DraculaBurning:
                {
                    draculaMovement.SunMove();
                    break;
                }
            case PlayerStates.TransformToBat:
                {
                    playerManeger.ActivateBat();
                    break;
                }
            case PlayerStates.BatDefault:
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

