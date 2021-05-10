using UnityEngine;

[CreateAssetMenu(fileName = "DisposeOfBody", menuName = "AI/Action/DisposeOfBody")]
public class DisposeOfBody : Action
{
	public override void Execute(ICharacter character)
	{
		PlayerManager.instance.NotoriousLevel.AddPlLongtSuspiciousLevel(1);
		character.DeadNpc.Disposed = true;
		character.DeadNpc.Dispose();
		character.SetAlertness(character.Stats.MaxAlerted / 2);
	}
}
