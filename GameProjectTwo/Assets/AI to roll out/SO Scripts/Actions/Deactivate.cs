using UnityEngine;

[CreateAssetMenu(fileName = "Deactivate", menuName = "AI/Action/Deactivate")]
public class Deactivate : Action
{
	public override void Execute(ICharacter character)
	{
		if (character.Self.CompareTag("Guard"))
		{
			NPCSpawner.Instance.NpcDespawn(false, character.Self);
		}
		else
		{
			NPCSpawner.Instance.NpcDespawn(true, character.Self);

			if (!character.Stationary)
			{
				return;
			}
			if (!character.IsDead)
			{
				return;
			}
			NPCSpawner.Instance.RemoveStationaryNPC(character.Self);
		}
	}
}
