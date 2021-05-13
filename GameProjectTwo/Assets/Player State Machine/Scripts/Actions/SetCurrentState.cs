using UnityEngine;

[CreateAssetMenu(fileName = "SetCurrentState", menuName = "Player/Action/SetCurrentState")]
public class SetCurrentState : PlayerAction
{
	[SerializeField] PlayerStates state;
	public override void Execute(IPlayer player)
	{
		player.currentState = state;
	}
}
