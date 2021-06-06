using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer
{
    public class DErrorUI : MonoBehaviour
    {
        PIDController pidController;
        Text text;
        string baseText = "D Error: ";
        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            pidController = FindObjectOfType<PIDController>();
        }

        private void Update()
        {
            text.text = string.Join("", baseText, pidController.GetDError().ToString());
        }
    }
}
