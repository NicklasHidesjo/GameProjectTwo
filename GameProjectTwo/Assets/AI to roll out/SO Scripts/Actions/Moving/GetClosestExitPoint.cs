using UnityEngine;

[CreateAssetMenu(fileName = "GetClosestExitPoint", menuName = "AI/Action/GetClosestExitPoint")]
public class GetClosestExitPoint : Action
{
	public override void Execute(ICharacter character)
	{
		GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("EscapePoint");
		if(exitPoints.Length < 1) 
		{
			return; 
		}

		Vector3 closestEscape = exitPoints[0].transform.position;
		Vector3 characterPosition = character.Transform.position;
		if (exitPoints.Length > 1)
		{
			float currentClosest = Vector3.Distance(closestEscape, characterPosition);
			foreach (var point in exitPoints)
			{
				float distance = Vector3.Distance(point.transform.position, characterPosition);
				if(distance < currentClosest)
				{
					currentClosest = distance;
					closestEscape = point.transform.position;
				}
			}
		}
		character.Agent.destination = closestEscape;
	}
}
