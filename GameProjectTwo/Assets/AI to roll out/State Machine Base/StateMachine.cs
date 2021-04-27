using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(NPC))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State currentState;
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

        if (character.Alertness > character.Stats.CautiousThreshold)
        {
            //print(name + " has entered " + newState);
        }

        currentState = Instantiate(newState);
        currentState.Enter(this, character);
    }
}
