using System;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotWheelController : MonoBehaviour
    {
        public float maxWheelSpeed;

        TBotCommon tbc;
        Vector2 totalVelocity;
        float modelWheelSpeed = 1.05f;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void UpdateTargetVelocity()
        {
            ClampAndNormalizeTotalVelocity();
            JointMotor leftMotor = tbc.LeftWheelHinge.motor;
            JointMotor rightMotor = tbc.RightWheelHinge.motor;
            leftMotor.targetVelocity = -totalVelocity.x;
            rightMotor.targetVelocity = -totalVelocity.y;
            tbc.LeftWheelHinge.motor = leftMotor;
            tbc.RightWheelHinge.motor = rightMotor;
        }

        private void ClampAndNormalizeTotalVelocity()
        {
            float largestVelSent = totalVelocity.x > totalVelocity.y ?
                Mathf.Abs(totalVelocity.x) :
                Mathf.Abs(totalVelocity.y);
            if (largestVelSent > maxWheelSpeed)
            {
                totalVelocity = totalVelocity / largestVelSent * maxWheelSpeed;
            }
        }

        private void LateUpdate()
        {
            UpdateTargetVelocity();
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
            totalVelocity.y *= modelWheelSpeed;
        }

        public void TurnRight()
        {
            SendVelocityCommand(new Vector2(maxWheelSpeed, -maxWheelSpeed));
        }

        public void TurnLeft()
        {
            SendVelocityCommand(new Vector2(-maxWheelSpeed, maxWheelSpeed));
        }

        public void GoForward()
        {
            SendVelocityCommand(new Vector2(maxWheelSpeed, maxWheelSpeed));
        }

        public void Reverse()
        {
            SendVelocityCommand(new Vector2(-maxWheelSpeed, -maxWheelSpeed));
        }

        public void Stop()
        {
            SendVelocityCommand(new Vector2(0, 0));
        }

    }
}
