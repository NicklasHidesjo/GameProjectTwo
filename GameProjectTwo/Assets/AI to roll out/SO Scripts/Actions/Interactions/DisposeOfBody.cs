using UnityEngine;

[CreateAssetMenu(fileName = "DisposeOfBody", menuName = "AI/Action/DisposeOfBody")]
public class DisposeOfBody : Action
{
	public override void Execute(ICharacter character)
	{
		Debug.Log("A guard has disposed of a body");
		character.DeadNpc.Disposed = true;
		character.DeadNpc.gameObject.SetActive(false);
		character.SetAlertness(character.Stats.MaxAlerted / 2);
	}
}
