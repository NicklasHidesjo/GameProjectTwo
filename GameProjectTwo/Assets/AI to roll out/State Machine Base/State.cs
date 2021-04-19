using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State",order = 1)]
public class State : ScriptableObject
{
	[Tooltip("These actions will only execute once when we enter the state")]
	[SerializeField] Action[] enterActions;
	[Tooltip("These actions will execute every fixedUpdate")]
	[SerializeField] Action[] executeActions;
	[Tooltip("These actions will execute when we exit the state")]
	[SerializeField] Action[] exitActions;
	[Tooltip("The Transition Points that we can use.")]
	[SerializeField] Transition[] transitions;

	private StateMachine stateMachine;
	private ICharacter character;

	public void Enter(StateMachine stateMachine, ICharacter character)
	{
		this.stateMachine = stateMachine;
		this.character = character;
		ExecuteEnterActions();
	}

	public void ExecuteState()
	{
		ExecuteActions();
		ExecuteTransitions();
	}
	public void Exit()
	{
		ExecuteExitActions();
	}

	private void ExecuteEnterActions()
	{
		foreach (var action in enterActions)
		{
			action.Execute(character);
		}
	}
	private void ExecuteActions()
	{
		foreach (var action in executeActions)
		{
			action.Execute(character);
		}
	}
	private void ExecuteTransitions()
	{
		foreach (var transition in transitions)
		{
			if(transition.decision.Decide(character))
			{
				stateMachine.SetState(transition.newState);
				break;
			}
		}
	}
	private void ExecuteExitActions()
	{
		foreach (var action in exitActions)
		{
			action.Execute(character);
		}
	}

}
