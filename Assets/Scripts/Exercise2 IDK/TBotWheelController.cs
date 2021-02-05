using System;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotWheelController : MonoBehaviour
    {
        [SerializeField]
        float maxWheelSpeed;

        TBotCommon tbc;
        Vector2 totalVelocity;

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
            bool lOver = totalVelocity.x > maxWheelSpeed;
            bool rOver = totalVelocity.y > maxWheelSpeed;
            if (lOver && rOver)
            {
                if (totalVelocity.x > totalVelocity.y)
                {
                    totalVelocity /= totalVelocity.x;
                }
                else
                {
                    totalVelocity /= totalVelocity.y;
                }
                totalVelocity *= maxWheelSpeed;
            }
            else if (rOver)
            {
                totalVelocity /= totalVelocity.y;
                totalVelocity *= maxWheelSpeed;
            }
            else if (lOver)
            {
                totalVelocity /= totalVelocity.x;
                totalVelocity *= maxWheelSpeed;
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
        }

        public void TurnRight()
        {
            SendVelocityCommand(new Vector2(maxWheelSpeed, 0));
        }

        public void TurnLeft()
        {
            SendVelocityCommand(new Vector2(0, maxWheelSpeed));
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
