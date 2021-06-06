using UnityEngine;

namespace RoboticsPrimer.ExercisePID
{
    public class Motor : MonoBehaviour
    {
#region MEMBERS
        HingeJoint m_hingeJoint;
        float m_lastAngle = 0;
        int m_numRotations;
#endregion
#region ENGINE
        void Start()
        {
            m_hingeJoint = GetComponent<HingeJoint>();
            ResetMotor();
        }

        void FixedUpdate()
        {
            UpdateNumRotations();
        }
#endregion 
#region PUBLIC FUNCTIONS
        public float GetMotorFullAngle()
        {
            return GetAdjustedHingeAngle() + m_numRotations * 360;
        }

        public float GetVelocity()
        {
            return m_hingeJoint.velocity;
        }
#endregion
#region HELPERS AND UI
        public void ResetMotor()
        {
            m_numRotations = 0;
            m_lastAngle = GetAdjustedHingeAngle();
        }

        private void UpdateNumRotations()
        {
            float adjustedAngle = GetAdjustedHingeAngle();
            float angleDiff = adjustedAngle - m_lastAngle;
            if (angleDiff > 100)
            {
                --m_numRotations;
            }
            else if (angleDiff < -100)
            {
                ++m_numRotations;
            }
            m_lastAngle = adjustedAngle;
        }

        float GetAdjustedHingeAngle()
        {
            return m_hingeJoint.angle + 180;
        }
#endregion
    }
}
