using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalktToRandomPathPoint", menuName = "AI/Action/WalktToRandomPathPoint")]
public class WalktToRandomPathPoint : Action
{
	public override void Execute(ICharacter character)
	{
		character.Move(character.Path[Random.Range(0, character.Path.Length)].position);
	}
}
