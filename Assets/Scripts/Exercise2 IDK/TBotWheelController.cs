using UnityEngine;

namespace RoboticsPrimer { 
    public class TBotWheelController : MonoBehaviour
    {
        [SerializeField]
        float leftWheelVelocity, rightWheelVelocity, baseSpeed;

        TBotCommon tbc;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void FixedUpdate()
        {
            SendVelocityCommand(tbc.RightWheelHinge, leftWheelVelocity);
            SendVelocityCommand(tbc.LeftWheelHinge, rightWheelVelocity);
        }

        public void SendVelocityCommand(HingeJoint wheelHinge, float targetVelocity)
        {
            JointMotor motor = wheelHinge.motor;
            motor.targetVelocity = -targetVelocity;
            wheelHinge.motor = motor;
        }

        public void TurnRight()
        {
            SendVelocityCommand(tbc.RightWheelHinge, 0);
            SendVelocityCommand(tbc.LeftWheelHinge, baseSpeed);
        }

        public void TurnLeft()
        {
            SendVelocityCommand(tbc.LeftWheelHinge, 0);
            SendVelocityCommand(tbc.RightWheelHinge, baseSpeed);
        }

        public void GoForward()
        {
            SendVelocityCommand(tbc.RightWheelHinge, baseSpeed);
            SendVelocityCommand(tbc.LeftWheelHinge, baseSpeed);
        }

        public void Reverse()
        {
            SendVelocityCommand(tbc.RightWheelHinge, -baseSpeed);
            SendVelocityCommand(tbc.LeftWheelHinge, -baseSpeed);
        }

        public void Stop()
        {
            SendVelocityCommand(tbc.LeftWheelHinge, 0);
            SendVelocityCommand(tbc.RightWheelHinge, 0);
        }

    }
}
