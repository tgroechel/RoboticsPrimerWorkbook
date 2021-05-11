using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer {
    public class KPUI : MonoBehaviour {
        PIDController pidController;
        Slider slider;
        Text text;
        string baseText = "KP: ";
        private void Awake() {
            slider = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<Text>();
            pidController = FindObjectOfType<PIDController>();
            slider.onValueChanged.AddListener(UpdateTextToValue);
            slider.onValueChanged.AddListener(UpdateKP);
            slider.value = pidController.kp;
        }

        private void UpdateKP(float arg0) {
            pidController.kp = arg0;
        }

        private void UpdateTextToValue(float arg0) {
            text.text = string.Join("", baseText, arg0.ToString());
        }
    }
}
