using UnityEngine;

[CreateAssetMenu(fileName = "StartDisposing", menuName = "AI/Decision/StartDisposing")]
public class StartDisposing : Decision
{
	public override bool Decide(ICharacter character)
	{
		return Vector3.Distance(character.Transform.position, character.DeadNpc.transform.position) < character.Agent.stoppingDistance + 0.5f;
	}
}
