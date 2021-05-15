using UnityEngine;

[CreateAssetMenu(fileName = "Crouch", menuName = "Player/Action/Crouch")]
public class Crouch : PlayerAction
{
    public override void Execute(IPlayer player)
    {
        player.Controller.radius = 0.25f;
        player.Controller.height = 0.5f;
        player.Controller.Move(-Vector3.up * 0.76f);
    }
}
