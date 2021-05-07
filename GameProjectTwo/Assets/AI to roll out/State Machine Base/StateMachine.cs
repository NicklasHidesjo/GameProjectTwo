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

    bool inactive = false;

    private void OnEnable()
    {
        character = GetComponent<ICharacter>();
        if (character.Stationary)
        {
            character.InitializeNPC();
            InitializeStateMachine();
        }
    }

    public void InitializeStateMachine()
	{
        inactive = false;
        SetState(startingState);
    }

    void FixedUpdate()
    {
        if(inactive)
		{
            return;
		}
        if(character.IsDead)
		{
            Debug.Log(name + " is dead");
            return;
		}
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

        currentState = Instantiate(newState);
        currentState.Enter(this, character);
    }

	private void OnDisable()
	{
        inactive = true;
	}
}
