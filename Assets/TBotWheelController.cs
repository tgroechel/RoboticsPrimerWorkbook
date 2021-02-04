using UnityEngine;

public class TBotWheelController : MonoBehaviour
{
    [SerializeField]
    float leftWheelVelocity, rightWheelVelocity;

    HingeJoint leftWheelHinge, rightWheelHinge;
    static string baseFootprintName = "base_footprint", baseLinkName = "base_link",
        leftWheelLinkName = "wheel_left_link", rightWheelLinkName = "wheel_right_link";

    private void Awake()
    {
        leftWheelHinge = transform.Find(GetQualifiedLinkName(baseFootprintName, baseLinkName, leftWheelLinkName)).GetComponent<HingeJoint>();
        rightWheelHinge = transform.Find(GetQualifiedLinkName(baseFootprintName, baseLinkName, rightWheelLinkName)).GetComponent<HingeJoint>();
    }


    private void FixedUpdate()
    {
        SendVelocityCommand(leftWheelHinge, leftWheelVelocity);
        SendVelocityCommand(rightWheelHinge, rightWheelVelocity);
    }

    public void SendVelocityCommand(HingeJoint wheelHinge, float targetVelocity)
    {
        JointMotor motor = wheelHinge.motor;
        motor.targetVelocity = targetVelocity;
        wheelHinge.motor = motor;
    }


    string GetQualifiedLinkName(params string[] links)
    {
        return string.Join("/", links);
    }

}
