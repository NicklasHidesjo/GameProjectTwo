using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StartWalking" ,menuName = "AI/Decision/StartWalking")]
public class StartWalking : Decision
{ 
	public override bool Decide(ICharacter character)
	{
		return character.StateTime >= character.Stats.IdleTime;
	}
}
