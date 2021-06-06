using UnityEngine;

namespace RoboticsPrimer.ExercisePID
{
    public class PIDController : MonoBehaviour
    {
#region MEMBERS
        [SerializeField]
        public float kp, kd, ki, desiredAngle;
        [SerializeField]
        float lastDesiredAngle, pError, iError, dError;

        Motor motor;
        Rigidbody rigidBody;
        Vector3 forceLocation, forceDirection;
#endregion

#region ENGINE
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
#endregion

#region CODE

        /// <summary>
        /// CODE: Adds motor force calculated from the different gains.
        /// Called from `FixedUpdate` every frame
        /// `forceDirection` will help here
        /// The `DrawDebugLine` draws the force vector at the top of the pendulum
        /// </summary>
        private void UpdateMotorForce()
        {
            Vector3 forceVec = Vector3.zero;

       
            forceVec += forceDirection * CalculatePGain();
            forceVec += forceDirection * CalculateIGain();
            forceVec += forceDirection * CalculateDGain();
            

            DrawDebugLine(forceVec, forceLocation);
            rigidBody.AddForceAtPosition(forceVec, forceLocation);
        }


        /// <summary>
        /// CODE: calculates the P Error using the motor and desired angle
        /// </summary>
        /// <returns>P error</returns>
        public float GetPError()
        {
  

            return desiredAngle - motor.GetFullyRotatedHingeAngle();

        }

        /// <summary>
        /// CODE: calculates the propotional gain using PError
        /// </summary>
        /// <returns>Calculated P Gain</returns>
        private float CalculatePGain()
        {


            pError = GetPError();
            return kp * pError;

        }

        /// <summary>
        /// CODE: returns the Integrated Error
        /// </summary>
        /// <returns>Integrated Error</returns>
        public float GetIError()
        {

            return iError;

        }

        /// <summary>
        /// CODE: Updates and returns the I Gain
        /// </summary>
        /// <returns>I Gain</returns>
        private float CalculateIGain()
        {

            iError += pError;
            return ki * iError;

        }

        /// <summary>
        /// CODE: returns the derivative error using the motor
        /// </summary>
        /// <returns>Derivative error</returns>
        public float GetDError()
        {
            return 0;

            return -motor.GetVelocity();

        }

        /// <summary>
        /// CODE: returns the D gain
        /// </summary>
        /// <returns>D Gain</returns>
        private float CalculateDGain()
        {
            return 0;

            dError = GetDError();
            return kd * dError;

        }
#endregion

#region HELPERS
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
            desiredAngle = 0;
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
#endregion
    }
}