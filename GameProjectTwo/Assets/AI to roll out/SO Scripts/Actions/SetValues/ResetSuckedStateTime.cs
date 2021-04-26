using UnityEngine;

[CreateAssetMenu(fileName = "ResetSuckedStateTime", menuName = "AI/Action/ResetSuckedStateTime")]
public class ResetSuckedStateTime : Action
{
	public override void Execute(ICharacter character)
	{
        if(!character.GettingSucked)
		{
			return;
		}
		character.StateTime = 0;
	}
}
