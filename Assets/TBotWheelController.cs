using UnityEngine;

namespace RoboticsPrimer { 
    public class TBotWheelController : MonoBehaviour
    {
        [SerializeField]
        float leftWheelVelocity, rightWheelVelocity;

        TBotCommon tbc;


        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void FixedUpdate()
        {
            SendVelocityCommand(tbc.RightWheelHinge, leftWheelVelocity);
            SendVelocityCommand(tbc.LeftWheelHinge, rightWheelVelocity);
            Debug.Log(tbc.GetPosition());
        }

        public void SendVelocityCommand(HingeJoint wheelHinge, float targetVelocity)
        {
            JointMotor motor = wheelHinge.motor;
            motor.targetVelocity = targetVelocity;
            wheelHinge.motor = motor;
        }

    }
}
