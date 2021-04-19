using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseStateTime" , menuName = "AI/Action/IncreaseStateTime")]
public class IncreaseStateTime : Action
{
	public override void Execute(ICharacter character)
	{
		character.StateTime += Time.fixedDeltaTime;
	}
}
