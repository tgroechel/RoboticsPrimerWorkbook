using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace ExercisePID
{
    public class ResetPIDScene : MonoBehaviour
    {
        Motor m_motor;
        PIDController m_PIDController;

        private void Start()
        {
            m_motor = GetComponent<Motor>();
            m_PIDController = GetComponentInChildren<PIDController>();
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Alpha0))
            {
                m_motor.ResetMotor();
                m_PIDController.ResetPID();
            }
        }
    }
}
