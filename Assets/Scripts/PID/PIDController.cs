using UnityEngine;

namespace RoboticsPrimer.ExercisePID
{
    public class PIDController : MonoBehaviour
    {
        [SerializeField]
        float kp, kd, ki, desiredAngle;
        [SerializeField]
        float lastDesiredAngle, pError, iError, dError;

        Motor motor;
        Rigidbody rigidBody;
        Vector3 forceLocation, forceDirection;

        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            motor = transform.parent.GetComponent<Motor>();
            ResetPID();
        }

        void FixedUpdate()
        {
            UpdateForceLocationAndDirection();
            UpdateIErrorOnChangeOfDesiredAngle();
            UpdateMotorForce();
        }


        private void UpdateMotorForce()
        {
            Vector3 forceVec = Vector3.zero;
            forceVec += forceDirection * CalculatePGain();
            forceVec += forceDirection * CalculateIGain();
            forceVec += forceDirection * CalculateDGain();

            DrawDebugLine(forceVec, forceLocation);
            rigidBody.AddForceAtPosition(forceVec, forceLocation);
        }

        private float CalculatePGain()
        {
            pError = desiredAngle - motor.GetFullyRotatedHingeAngle();
            return kp * pError;
        }


        private float CalculateIGain()
        {
            iError += pError;
            return ki * iError;
        }

        private float CalculateDGain()
        {
            dError = -motor.GetVelocity();
            return kd * dError;
        }


        /// <summary>
        /// Recalculates force location and direction
        /// </summary>
        private void UpdateForceLocationAndDirection()
        {
            forceLocation = transform.up * 2;
            forceDirection = -transform.forward;
        }

        /// <summary>
        /// Resets integrated error when desiredAngle changes
        /// </summary>
        private void UpdateIErrorOnChangeOfDesiredAngle()
        {
            if (!Mathf.Approximately(desiredAngle, lastDesiredAngle))
            {
                iError = 0;
            }
            lastDesiredAngle = desiredAngle;
        }

        /// <summary>
        /// Resets velocity and errors of pendulum
        /// </summary>
        public void ResetPID()
        {
            iError = 0;
            rigidBody.velocity = Vector3.zero;
            lastDesiredAngle = desiredAngle;
        }

        /// <summary>
        /// Draws force line normalized
        /// </summary>
        /// <param name="force">Direction of force vector, will be normalized for drawing</param>
        /// <param name="forceLocation">Local position of Force vector</param>
        void DrawDebugLine(Vector3 force, Vector3 forceLocation)
        {
            Debug.DrawRay(transform.position + forceLocation,
            Vector3.Normalize(force),
            Color.red,
             Time.deltaTime);
        }
    }
}