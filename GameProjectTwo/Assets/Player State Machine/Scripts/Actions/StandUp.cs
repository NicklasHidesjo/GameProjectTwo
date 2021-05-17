using UnityEngine;

[CreateAssetMenu(fileName = "StandUp", menuName = "Player/Action/StandUp")]
public class StandUp : PlayerAction
{
    public override void Execute(IPlayer player)
    {
        player.Controller.height = 2;
        player.Controller.radius = 0.5f;
        player.Controller.Move(Vector3.up * 0.75f);
    }
}
