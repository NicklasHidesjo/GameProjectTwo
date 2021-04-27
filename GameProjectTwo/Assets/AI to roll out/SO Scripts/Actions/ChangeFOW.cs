using UnityEngine;

[CreateAssetMenu(fileName = "ChangeFOW", menuName = "AI/Action/ChangeFOW")]
public class ChangeFOW : Action
{
	[SerializeField] bool alertedFOW;
	public override void Execute(ICharacter character)
	{
        if(alertedFOW)
		{
			character.FOV = character.Stats.AlertedFOV;
		}
		else
		{
			character.FOV = character.Stats.RelaxedFOV;
		}
	}
}
