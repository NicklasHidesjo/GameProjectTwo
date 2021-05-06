using UnityEngine;

[CreateAssetMenu(fileName = "DoneDisposing", menuName = "AI/Decision/DoneDisposing")]
public class DoneDisposing : Decision
{
	public override bool Decide(ICharacter character)
	{
		return !character.DeadNpc.gameObject.activeSelf;
	}
}
