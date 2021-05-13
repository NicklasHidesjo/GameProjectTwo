using UnityEngine;
public abstract class PlayerAction : ScriptableObject
{
	public abstract void Execute(IPlayer player);
}
