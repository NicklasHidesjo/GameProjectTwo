using UnityEngine;

public abstract class PlayerDecision: ScriptableObject
{
	public abstract bool Decide(IPlayer player);
}
