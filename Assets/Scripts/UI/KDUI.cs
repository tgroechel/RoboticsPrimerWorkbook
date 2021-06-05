using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer
{
    public class KDUI : MonoBehaviour
    {
        PIDController pidController;
        Slider slider;
        Text text;
        string baseText = "KD: ";
        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<Text>();
            pidController = FindObjectOfType<PIDController>();
            slider.onValueChanged.AddListener(UpdateTextToValue);
            slider.onValueChanged.AddListener(UpdateKD);
            slider.value = pidController.kd;
        }

        private void UpdateKD(float arg0)
        {
            pidController.kd = arg0;
        }

        private void UpdateTextToValue(float arg0)
        {
            text.text = string.Join("", baseText, arg0.ToString());
        }
    }
}
