using UnityEngine;

[CreateAssetMenu(fileName = "ToogleSearchAgentRotation", menuName = "AI/Action/ToogleSearchAgentRotation")]
public class ToogleSearchAgentRotation : Action
{
	public override void Execute(ICharacter character)
	{
        if(character.RotationStarted)
		{
			character.Agent.updateRotation = false;
			return;
		}
		character.Agent.updateRotation = true;

	}
}
