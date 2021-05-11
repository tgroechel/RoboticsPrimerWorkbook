using UnityEngine;

namespace RoboticsPrimer.ExercisePID {
    public class ResetPIDScene : MonoBehaviour {
        Motor motor;
        PIDController pidController;
        DesiredAngleUI desiredAngleUI;

        private void Start() {
            motor = GetComponent<Motor>();
            pidController = GetComponentInChildren<PIDController>();
            desiredAngleUI = FindObjectOfType<DesiredAngleUI>();
        }

        public void ResetPIDAndMotor() {
            motor.ResetMotor();
            pidController.ResetPID();
            desiredAngleUI.Reset();
        }
    }
}
