using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IncreasePathIndex", menuName = "AI/Action/IncreasePathIndex")]
public class IncreasePathIndex : Action
{
	public override void Execute(ICharacter character)
	{
		if(character.Increase)
		{
			character.PathIndex += 1;
		}
		else
		{
			character.PathIndex -= 1;
		}

		if(character.PathIndex >= character.Path.Count)
		{
			if (character.BackTrack)
			{
				character.Increase = !character.Increase;
				character.PathIndex = character.Path.Count -2;
			}
			else
			{
				character.PathIndex = 0;
			}
		}
		if (character.PathIndex < 0)
		{
			if(character.BackTrack)
			{
				character.Increase = !character.Increase;
				character.PathIndex = 1;
			}
			else
			{
				character.PathIndex = character.Path.Count - 1;
			}
		}
	}
}
