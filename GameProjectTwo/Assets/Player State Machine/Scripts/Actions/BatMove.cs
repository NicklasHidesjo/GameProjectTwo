using UnityEngine;

[CreateAssetMenu(fileName = "BatMove", menuName = "Player/Action/BatMove")]
public class BatMove : PlayerAction
{
    Vector3 playerVelocity;
    CharacterController controller;
    PlayerStats stats;
    Transform transform;

	public override void Execute(IPlayer player)
	{
        controller = player.Controller;
        stats = player.Stats;
        transform = player.Transform;
        playerVelocity = BatControl(player);
        controller.Move(playerVelocity * Time.deltaTime);
        SetFaceForward();
    }

    private void SetFaceForward()
    {
        transform.rotation = Quaternion.LookRotation(playerVelocity) * Quaternion.Euler(0, 0, -stats.BankAmount * Input.GetAxis("Horizontal"));
    }
    Vector3 BatControl(IPlayer player)
    {
        Vector3 controllerDir = Quaternion.Euler(0, Input.GetAxis("Horizontal") * stats.SteerSpeed * Time.deltaTime, 0) * transform.forward;
        controllerDir = (controllerDir) * player.Speed;
        controllerDir.y = Mathf.Clamp(SphereCastGround(), 0, 3);
        return controllerDir;
    }
    float SphereCastGround()
    {
		float yAxisSmoothAdjust = playerVelocity.y;

		if (Physics.SphereCast(transform.position + Vector3.up * controller.radius, controller.radius, -Vector3.up, out RaycastHit hit, stats.FlightHeight, stats.CheckLayerForFlight))
        {
            //WARNING : Checking for standing still (Never happens)
            if (hit.point != transform.position - Vector3.up * stats.FlightHeight)
            {
                Vector3 desiredPos = hit.point + Vector3.up * hit.normal.y * stats.FlightHeight;
                float hightOff = 1 / stats.FlightHeight * (desiredPos.y - transform.position.y);

                hightOff *= hightOff;

                yAxisSmoothAdjust += hightOff + (Mathf.Abs(playerVelocity.y)) * Time.deltaTime;
                yAxisSmoothAdjust *= 1 - stats.Damping * Time.deltaTime;

            }
        }
        else
        {
            yAxisSmoothAdjust -= stats.DownForce * Time.deltaTime;
        }

        return yAxisSmoothAdjust;
    }
}
