using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotWheelController : MonoBehaviour
    {
        [SerializeField]
        float baseSpeed;

        TBotCommon tbc;
        Vector2 totalVelocity;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void UpdateTargetVelocity()
        {
            JointMotor leftMotor = tbc.LeftWheelHinge.motor;
            JointMotor rightMotor = tbc.RightWheelHinge.motor;
            leftMotor.targetVelocity = -totalVelocity.x;
            rightMotor.targetVelocity = -totalVelocity.y;
            tbc.LeftWheelHinge.motor = leftMotor;
            tbc.RightWheelHinge.motor = rightMotor;
        }

        public void SendVelocityCommand(Vector2 velVec, bool additiveVelocity = false)
        {
            if (additiveVelocity)
            {
                totalVelocity += velVec;
            }
            else
            {
                totalVelocity = velVec;
            }
            UpdateTargetVelocity();
        }

        public void TurnRight()
        {
            SendVelocityCommand(new Vector2(baseSpeed, 0));
        }

        public void TurnLeft()
        {
            SendVelocityCommand(new Vector2(0, baseSpeed));
        }

        public void GoForward()
        {
            SendVelocityCommand(new Vector2(baseSpeed, baseSpeed));
        }

        public void Reverse()
        {
            SendVelocityCommand(new Vector2(-baseSpeed, -baseSpeed));
        }

        public void Stop()
        {
            SendVelocityCommand(new Vector2(0, 0));
        }

    }
}
