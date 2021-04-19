using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    [SerializeField] State startingState;
    ICharacter character;

    void Start()
    {
        character = GetComponent<ICharacter>();
        SetState(startingState);
    }


    void FixedUpdate()
    {
        if (currentState != null)
        {
            Debug.Log(currentState); // remove this once testing is completed
            currentState.ExecuteState();
        }
    }


    public void SetState(State newState)
    {
        if(newState == null) { Debug.LogError("No state found"); return; }

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this, character);
    }
}
