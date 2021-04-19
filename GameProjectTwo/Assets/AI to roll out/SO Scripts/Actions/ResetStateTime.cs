using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResetStateTime" ,menuName = "AI/Action/ResetStateTime")]
public class ResetStateTime : Action
{
	public override void Execute(ICharacter character)
	{
		character.StateTime = 0;
	}
}
