using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer { 
    public class TBotTurnGoTurn : MonoBehaviour
    {
        [SerializeField]
        Transform goal;

        [SerializeField]
        float speed;

        TBotCommon tbc;
        TBotWheelController tbWheelController;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
            tbWheelController = GetComponent<TBotWheelController>();
        }

        private void Update()
        {
            Debug.DrawLine(tbc.Position, tbc.Position + tbc.Heading, Color.green);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                TurnRight();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                TurnLeft();
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                GoForward();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Reverse();
            }
            else
            {
                Stop();
            }
        }

        void TurnRight()
        {
            tbWheelController.SendVelocityCommand(tbc.RightWheelHinge, 0);
            tbWheelController.SendVelocityCommand(tbc.LeftWheelHinge, speed);
        }

        void TurnLeft()
        {
            tbWheelController.SendVelocityCommand(tbc.LeftWheelHinge, 0);
            tbWheelController.SendVelocityCommand(tbc.RightWheelHinge, speed);
        }

        void GoForward()
        {
            tbWheelController.SendVelocityCommand(tbc.RightWheelHinge, speed);
            tbWheelController.SendVelocityCommand(tbc.LeftWheelHinge, speed); 
        }

        void Reverse()
        {
            tbWheelController.SendVelocityCommand(tbc.RightWheelHinge, -speed);
            tbWheelController.SendVelocityCommand(tbc.LeftWheelHinge, -speed);
        }

        void Stop()
        {
            tbWheelController.SendVelocityCommand(tbc.LeftWheelHinge, 0);
            tbWheelController.SendVelocityCommand(tbc.RightWheelHinge, 0);
        }
    }
}
