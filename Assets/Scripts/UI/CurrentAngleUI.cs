using RoboticsPrimer.ExercisePID;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer
{
    public class CurrentAngleUI : MonoBehaviour
    {
        Motor motor;
        Text text;
        string baseText = "Current Angle: ";
        private void Awake()
        {
            text = GetComponentInChildren<Text>();
            motor = FindObjectOfType<Motor>();
        }

        private void Update()
        {
            text.text = string.Join("", baseText, motor.GetFullyRotatedHingeAngle().ToString());
        }
    }
}
