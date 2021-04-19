using UnityEngine;

[CreateAssetMenu(fileName = "SetSpeed", menuName = "AI/Action/SetSpeed")]
public class SetSpeed : Action
{
	[SerializeField] NPCSpeeds setSpeed;
	public override void Execute(ICharacter character)
	{
		switch (setSpeed)
		{
			case NPCSpeeds.walk:
				character.Agent.speed = character.Stats.WalkSpeed;
				break;
			case NPCSpeeds.suspiscious:
				character.Agent.speed = character.Stats.SuspisciousSpeed;
				break;
			case NPCSpeeds.run:
				character.Agent.speed = character.Stats.RunSpeed;
				break;
			default:
				break;
		}
	}
}
