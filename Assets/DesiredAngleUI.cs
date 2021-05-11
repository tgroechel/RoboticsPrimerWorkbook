using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer {
    public class DesiredAngleUI : MonoBehaviour {
        PIDController pidController;
        Slider slider;
        Text text;
        string baseText = "Desired Angle: ";
        private void Awake() {
            slider = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<Text>();
            pidController = FindObjectOfType<PIDController>();
            slider.onValueChanged.AddListener(UpdateTextToValue);
            slider.onValueChanged.AddListener(UpdateDesiredAngle);
            slider.value = pidController.desiredAngle;
        }

        public void Reset() {
            slider.value = pidController.desiredAngle;
        }

        private void UpdateDesiredAngle(float arg0) {
            pidController.desiredAngle = arg0;
        }

        private void UpdateTextToValue(float arg0) {
            text.text = string.Join("", baseText, arg0.ToString());
        }
    }
}
