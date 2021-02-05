using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotBasicKeyboardController : MonoBehaviour
    {
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
            Debug.Log(tbc.Velocity);
        }
    }
}
