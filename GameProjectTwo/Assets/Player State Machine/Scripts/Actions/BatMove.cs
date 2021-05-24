using UnityEngine;

[CreateAssetMenu(fileName = "BatMove", menuName = "Player/Action/BatMove")]
public class BatMove : PlayerAction
{
    CharacterController cc;
    Vector3 input;
    Vector3 batRotationVector = Vector3.up;
    Vector3 realInput = Vector3.zero;
    Transform cam;
    PlayerStats stats;
    float banking = 0;


    
    public override void Execute(IPlayer player)
    {
        //We set this again to make sure it works w. other code.
        batRotationVector = cc.transform.forward;

        cc = player.Controller;
        stats = player.Stats;
        cam = player.AlignCamera;

        InputRotateTowards();
        InputFlightHight();

        realInput.x = batRotationVector.x * stats.FlightSpeed;
        realInput.z = batRotationVector.z * stats.FlightSpeed;
        realInput.y = SphareCastGround(realInput);

        cc.Move(realInput * Time.deltaTime);


        AnimateBatRotation();
    }

    void InputRotateTowards()
    {
        //Rotates axis towards input
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");

        float x = Mathf.Abs(input.x);
        float z = Mathf.Abs(input.z);

        input.y = 1 - Mathf.Clamp01(x + z);
        input = input.normalized;
        input = AlignInput(input);
        batRotationVector = Vector3.RotateTowards(batRotationVector, input, stats.TurnSpeed * Time.deltaTime, 0.0f);

        //Y is set to zero to avoid extrem spining
        Vector3 brv = Quaternion.Euler(0, 90, 0) * batRotationVector;
        brv.y = 0;
        input.y = 0;
        banking = Vector3.Dot(brv, input);
    }

    Vector3 AlignInput(Vector3 input)
    {
        //aligns to cam
        Vector3 camFrw = cam.transform.forward;
        camFrw.y = 0;
        Quaternion align = Quaternion.LookRotation(camFrw);
        return (align * input);
    }

    void InputFlightHight()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stats.FlightHight = stats.MaxFlightHight;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            stats.FlightHight = stats.MinFlightHight;
        }
    }

    float SphareCastGround(Vector3 velocity)
    {
        RaycastHit hit;

        float yAxisSmoothAdjust = velocity.y;

        if (Physics.SphereCast(cc.transform.position + Vector3.up * cc.radius, cc.radius, -Vector3.up, out hit, stats.FlightHight, stats.CheckLayerForFlight))
        {
            //WARNING : Checking for standing still (Never happens)
            if (hit.point != cc.transform.position - Vector3.up * stats.FlightHight)
            {
                Vector3 desiredPos = hit.point + Vector3.up * hit.normal.y * stats.FlightHight;
                float hightOff = 1 / stats.FlightHight * (desiredPos.y - cc.transform.position.y);

                hightOff *= hightOff;

                yAxisSmoothAdjust += hightOff + (Mathf.Abs(velocity.y)) * Time.fixedDeltaTime;
                yAxisSmoothAdjust *= 1 - stats.Damping * Time.fixedDeltaTime;
            }
        }
        else
        {
            yAxisSmoothAdjust -= stats.DownForce * Time.fixedDeltaTime;
        }

        return yAxisSmoothAdjust;
    }

    void AnimateBatRotation()
    {
        //Look forward 
        cc.transform.rotation = Quaternion.LookRotation(batRotationVector, cc.transform.up + Vector3.up);
        cc.transform.rotation *= Quaternion.Euler(0, 0, -banking * 45);
    }
}
