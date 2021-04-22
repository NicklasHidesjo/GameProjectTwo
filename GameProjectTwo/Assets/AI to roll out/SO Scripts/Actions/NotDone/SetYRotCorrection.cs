using UnityEngine;

[CreateAssetMenu(fileName = "SetYRotCorrection", menuName = "AI/Action/SetYRotCorrection")]
public class SetYRotCorrection : Action
{
	public override void Execute(ICharacter character)
	{
		if(character.RotationStarted) { return; }
		character.YRotCorrection = 0;

		bool hitLeft = GetHit(character, -character.Transform.right);
		bool hitRight = GetHit(character, character.Transform.right);
		bool hitFront = GetHit(character, character.Transform.forward);

		if (hitLeft && hitRight && hitFront)
		{
			character.YRotCorrection = character.Transform.rotation.y + 180;
		}
		else if (hitFront && hitLeft)
		{
			character.YRotCorrection = character.Transform.rotation.y + 135;
		}
		else if (hitFront && hitRight)
		{
			character.YRotCorrection = character.Transform.rotation.y - 135;
		}
		else if (hitRight && hitLeft)
		{
			character.YRotCorrection = 0;
		}
		else if (hitLeft)
		{
			character.YRotCorrection = character.Transform.rotation.y + 90;
		}
		else if (hitRight)
		{
			character.YRotCorrection = character.Transform.rotation.y - 90;
		}
		else if (hitFront)
		{
			character.YRotCorrection = character.Transform.rotation.y + 180;
		}


	}
	private bool GetHit(ICharacter character,Vector3 direction)
	{
		RaycastHit hit;
		if (Physics.Raycast(character.Transform.position, direction, out hit, character.Stats.ClearanceDistance))
		{
			if (hit.collider != null)
			{
				return true;
			}
		}
		return false;
	}
}
