using UnityEngine;

[CreateAssetMenu(menuName = "Player/State")]
public class PlayerState : ScriptableObject
{
	[Tooltip("These actions will only execute once when we enter the state")]
	[SerializeField] PlayerAction[] enterActions;
	[Tooltip("These actions will execute every fixedUpdate")]
	[SerializeField] PlayerAction[] executeActions;
	[Tooltip("These actions will execute when we exit the state")]
	[SerializeField] PlayerAction[] exitActions;
	[Tooltip("The Transition Points that we can use.")]
	[SerializeField] PlayerTransition[] transitions;

	private PlayerStateMachine stateMachine;
	private IPlayer player;

	public void Enter(PlayerStateMachine stateMachine, IPlayer player)
	{
		this.stateMachine = stateMachine;
		this.player = player;
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
			action.Execute(player);
		}
	}

	private void ExecuteActions()
	{
		foreach (var action in executeActions)
		{
			action.Execute(player);
		}
	}

	private void ExecuteTransitions()
	{
		foreach (var transition in transitions)
		{
			if(transition.decision.Decide(player))
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
			action.Execute(player);
		}
	}
}
