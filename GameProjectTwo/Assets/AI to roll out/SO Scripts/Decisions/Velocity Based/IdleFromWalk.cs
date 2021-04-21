using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleFromWalk", menuName ="AI/Decision/IdleFromWalk")]
public class IdleFromWalk : Decision
{
	public override bool Decide(ICharacter character)
	{
		return !character.Agent.pathPending && character.Agent.velocity == Vector3.zero;
	}
}
