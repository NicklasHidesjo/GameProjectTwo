using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum playerStates {Stoped, TransformToDracula, MoveDracula, Hidden, TransformToBat, FlyBat }
    private playerStates playerState;

    private PlayerManager playerManeger;
    private DraculaMovement draculaMovement;
    private BatMovement batMovement;

    public void SetScipts(PlayerManager playerManeger, DraculaMovement draculaMovement, BatMovement batMovement)
    {
        this.playerManeger = playerManeger;
        this.draculaMovement = draculaMovement;
        this.batMovement = batMovement;
    }

    public void SetState(playerStates newState)
    {
        playerState = newState;
    }
    public playerStates GetCurrentState()
    {
        return playerState;
    }
    public void UpdateByState()
    {
        switch (playerState)
        {
            case playerStates.Stoped:
                {
                    draculaMovement.UpdateCharacter();
                    Debug.Log("Disabled");
                    break;
                }
            case playerStates.TransformToDracula:
                {
                    playerManeger.ActivateDracula();
                    break;
                }
            case playerStates.MoveDracula:
                {
                    draculaMovement.MoveCharacter();
                    break;
                }
            case playerStates.Hidden:
                {
                    break;
                }
            case playerStates.TransformToBat:
                {
                    playerManeger.ActivateBat();
                    batMovement.StartMove(playerManeger.GetSpawnPoint().forward);
                    break;
                }
            case playerStates.FlyBat:
                {
                    batMovement.MoveBat();
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

