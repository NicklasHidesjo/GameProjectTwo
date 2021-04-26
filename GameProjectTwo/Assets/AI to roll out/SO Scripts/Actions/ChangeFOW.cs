using UnityEngine;

[CreateAssetMenu(fileName = "ChangeFOW", menuName = "AI/Action/ChangeFOW")]
public class ChangeFOW : Action
{
	[SerializeField] bool alertedFOW;
	public override void Execute(ICharacter character)
	{
        if(alertedFOW)
		{
			character.FOW = character.Stats.AlertedFOW;
		}
		else
		{
			character.FOW = character.Stats.RelaxedFOW;
		}
	}
}
