using UnityEngine;

namespace RoboticsPrimer.ExercisePID
{
    public class ResetPIDScene : MonoBehaviour
    {
        Motor motor;
        PIDController pidController;

        private void Start()
        {
            motor = GetComponent<Motor>();
            pidController = GetComponentInChildren<PIDController>();
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Alpha0))
            {
                motor.ResetMotor();
                pidController.ResetPID();
            }
        }
    }
}
