using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IncreasePathIndex", menuName = "AI/Action/IncreasePathIndex")]
public class IncreasePathIndex : Action
{
	public override void Execute(ICharacter character)
	{
		character.PathIndex += 1;
		if(character.PathIndex >= character.Path.Length)
		{
			character.PathIndex = 0;
		}
	}
}
