using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer { 
    public class TBotTurnGoTurn : MonoBehaviour
    {
        [SerializeField]
        Transform goal;

        TBotCommon tbc;
        TBotWheelController wheelController;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
            wheelController = GetComponent<TBotWheelController>();
        }

        private void FixedUpdate()
        {
            UpdateTurnGoTurn();

        }

        private void UpdateTurnGoTurn()
        {
            if (Vector3.Distance(goal.position, tbc.Position) < .1f)
            {
                return;
            }
            Debug.DrawLine(tbc.Position, tbc.Position + tbc.Heading, Color.green);
            Vector3 goalDirection = (goal.position - tbc.Position).normalized;
            float angle = Vector3.SignedAngle(goalDirection, tbc.Heading.normalized, Vector3.up);
            if (angle > 5)
            {
                wheelController.TurnLeft();
            }
            else if (angle < -5)
            {
                wheelController.TurnRight();
            }
            else
            {
                wheelController.GoForward();
            }
        }
    }
}
