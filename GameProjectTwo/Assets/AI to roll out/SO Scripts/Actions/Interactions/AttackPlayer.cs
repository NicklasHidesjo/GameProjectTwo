using UnityEngine;

[CreateAssetMenu(fileName = "AttackPlayer", menuName = "AI/Action/AttackPlayer")]
public class AttackPlayer : Action
{
	public override void Execute(ICharacter character)
	{
		character.TimeSinceLastAction += Time.fixedDeltaTime;
		if(character.TimeSinceLastAction >= 1/character.Stats.AttacksPerSecond)
		{
			character.TimeSinceLastAction = 0;
			character.Attack();
			// remove this once we play the sound using animation
			AudioManager.instance.PlaySound(SoundType.GuardAttack, character.Self.gameObject); 
		}
	}
}
