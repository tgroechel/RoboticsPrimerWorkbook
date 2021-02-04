using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExercisePID
{
    public class PIDController : MonoBehaviour
    {
        Rigidbody rigidBody;
        public float kp, kd, ki;
        public float desired_angle, last_desired_angle;
        float accumulated_error;
        Motor m_motor;
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            m_motor = transform.parent.GetComponent<Motor>();
            ResetPID();
        }

        void FixedUpdate()
        {
            UpdateAccumulatedError();
            Vector3 force_location = transform.up;
            Vector3 forcetoapply = -transform.forward;
            float error = desired_angle - m_motor.GetFullyRotatedHingeAngle();
            accumulated_error += error;
            Vector3 force_vec = forcetoapply *
                (kp * error +
                 kd * -m_motor.GetVelocity() +
                 ki * accumulated_error);
            DrawDebugLine(force_vec, force_location);
            rigidBody.AddForceAtPosition(force_vec,
                force_location);
        }

        private void UpdateAccumulatedError()
        {
            if (!Mathf.Approximately(desired_angle, last_desired_angle))
            {
                accumulated_error = 0;
            }
            last_desired_angle = desired_angle;
        }

        public void ResetPID()
        {
            accumulated_error = 0;
            rigidBody.velocity = Vector3.zero;
            last_desired_angle = desired_angle;
        }

        void DrawDebugLine(Vector3 force, Vector3 pos_loc)
        {
            Debug.DrawRay(transform.position + pos_loc,
            Vector3.Normalize(force),
            Color.red,
             Time.deltaTime);
        }
    }
}