using UnityEngine;

[CreateAssetMenu(fileName = "CheckGettingSucked", menuName = "AI/Action/CheckGettingSucked")]
public class CheckGettingSucked : Action
{
	public override void Execute(ICharacter character)
	{
		if(!character.GettingSucked) { return; }
		character.StateTime = 0;	
	}
}
