using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackedShout", menuName = "AI/Action/AttackedShout")]
public class AttackedShout : Action
{
	List<NPC> nearbyCharacters = new List<NPC>();
	public override void Execute(ICharacter character)
	{
		if (!character.ShouldShout) 
		{ 
			return; 
		}
		if(character.Alertness < character.Stats.CautiousThreshold)
		{
			return;
		}
		SetNearbyCharacters(character);

		foreach (var npc in nearbyCharacters)
		{
			if (npc == null)
			{
				continue;
			}
			npc.ReactToShout();
		}
	}

	private void SetNearbyCharacters(ICharacter character)
	{
		nearbyCharacters.Clear();
		Collider[] npcClose = Physics.OverlapSphere(character.Transform.position, character.Stats.AttackedShoutRange, character.NpcLayer);
		foreach (var npc in npcClose)
		{
			if (npc.GetComponent<NPC>() == character.Self)
			{ 
				continue; 
			}
			nearbyCharacters.Add(npc.GetComponent<NPC>());
		}
	}
}