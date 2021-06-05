using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer
{
    public class IErrorUI : MonoBehaviour
    {
        PIDController pidController;
        Text text;
        string baseText = "I Error: ";
        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            pidController = FindObjectOfType<PIDController>();
        }

        private void Update()
        {
            text.text = string.Join("", baseText, pidController.GetIError().ToString());
        }
    }
}
