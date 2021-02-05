using System;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotBasicKeyboardController : MonoBehaviour
    {
        [SerializeField]
        float speed;

        [SerializeField]
        bool useAdditiveSteering;

        TBotCommon tbc;
        TBotWheelController tbWheelController;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
            tbWheelController = GetComponent<TBotWheelController>();
        }

        private void Update()
        {
            if (useAdditiveSteering)
            {
                UpdateAdditiveSteering();
            }
            else
            {
                UpdateStaticSteering();
            }
            //Debug.Log(tbc.Velocity);
        }

        private void UpdateAdditiveSteering()
        {
            float left = 0, right = 0;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                left += speed;
                right -= speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                left -= speed;
                right += speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                left += speed;
                right += speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                left -= speed;
                right -= speed;
            }
            Debug.Log(left);
            tbWheelController.SendVelocityCommand(new Vector2(left, right));
        }

        private void UpdateStaticSteering()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                tbWheelController.TurnRight();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                tbWheelController.TurnLeft();
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                tbWheelController.GoForward();
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                tbWheelController.Reverse();
            }
            else
            {
                tbWheelController.Stop();
            }
        }
    }
}
