using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer {
    public class KIUI : MonoBehaviour {
        PIDController pidController;
        Slider slider;
        Text text;
        string baseText = "KI: ";
        private void Awake() {
            slider = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<Text>();
            pidController = FindObjectOfType<PIDController>();
            slider.onValueChanged.AddListener(UpdateTextToValue);
            slider.onValueChanged.AddListener(UpdateKI);
            slider.value = pidController.ki;
        }

        private void UpdateKI(float arg0) {
            pidController.ki = arg0;
        }

        private void UpdateTextToValue(float arg0) {
            text.text = string.Join("", baseText, arg0.ToString());
        }
    }
}
