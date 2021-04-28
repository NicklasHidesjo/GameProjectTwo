using UnityEngine;

[CreateAssetMenu(fileName = "InExitRange", menuName = "AI/Decision/InExitRange")]
public class InExitRange : Decision
{
	[Tooltip("This should be the layer that the exitpoint is on")]
	[SerializeField] LayerMask mask;
	public override bool Decide(ICharacter character)
	{
		if(character.Agent.velocity != Vector3.zero)
		{
			return false; 
		}
		float range = character.Agent.stoppingDistance + 0.5f;
		Collider[] closeExits = Physics.OverlapSphere(character.Transform.position, range, mask);

		return closeExits.Length > 0;
	}
}
