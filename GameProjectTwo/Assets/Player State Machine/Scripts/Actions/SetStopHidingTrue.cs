using UnityEngine;

[CreateAssetMenu(fileName = "SetStopHidingTrue", menuName = "Player/Action/SetStopHidingTrue")]
public class SetStopHidingTrue : PlayerAction
{
	public override void Execute(IPlayer player)
	{
        if(Input.GetButtonDown("Interact"))
		{
			player.StopHiding = true;
		}
		if(Input.GetButtonDown("Run"))
		{
			player.StopHiding = true;
		}
	}
}
