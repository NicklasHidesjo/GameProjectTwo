using UnityEngine;

[CreateAssetMenu(fileName = "CallForGuards", menuName = "AI/Action/CallForGuards")]
public class CallForGuards : Action
{
	public override void Execute(ICharacter character)
	{
        // get all characters in a radious of the player (shoutrange)
		// tell all the characters in that radius that they should 
		// check if they need to react (for a guard its always react for civilian 
		// if they are close enough (within reaction range)
		// in that method set that they should not shout
		// set so that the Should shout of each character called is false)
	}
}
