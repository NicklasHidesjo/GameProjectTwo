using UnityEngine;

[CreateAssetMenu(fileName = "SetIsCharmed", menuName = "AI/Action/SetIsCharmed")]
public class SetIsCharmed : Action
{
	public override void Execute(ICharacter character)
	{
        if(character.StateTime > character.Stats.CharmedTime)
		{
			character.IsCharmed = false;
		}
	}
}
