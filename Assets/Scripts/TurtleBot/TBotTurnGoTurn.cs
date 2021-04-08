using UnityEngine;

namespace RoboticsPrimer {
    public class TBotTurnGoTurn : MonoBehaviour {
        [SerializeField]
        Transform goal;

        TBotCommon tbc;
        TBotWheelController wheelController;
        float angleTolerance = 10, goalDistTolerance = 0.2f;

        private void Awake() {
            tbc = GetComponent<TBotCommon>();
            wheelController = GetComponent<TBotWheelController>();
        }

        private void FixedUpdate() {
            SetGoalOnGround();
            UpdateTurnGoTurn();
        }

        private void SetGoalOnGround() {
            if (Mathf.Approximately(goal.position.y, 0)) {
                goal.position = new Vector3(goal.position.x, 0, goal.position.z);
            }
        }

        public void UpdateGoalPosition(Vector2 pos) {
            UpdateGoalPosition(new Vector3(pos.x, 0, pos.y));
        }

        public void UpdateGoalPosition(Vector3 pos) {
            if (goal != null) {
                goal.position = pos;
            }
        }

        private void UpdateTurnGoTurn() {
            if (Vector3.Distance(goal.position, tbc.Position) < goalDistTolerance) {
                wheelController.Stop();
                return;
            }
            Debug.DrawLine(tbc.Position, tbc.Position + tbc.Heading, Color.green);
            Vector3 goalDirection = (goal.position - tbc.Position).normalized;
            float angle = Vector3.SignedAngle(goalDirection, tbc.Heading.normalized, Vector3.up);
            if (angle > angleTolerance) {
                wheelController.TurnLeft();
            }
            else if (angle < -angleTolerance) {
                wheelController.TurnRight();
            }
            else {
                wheelController.GoForward();
            }
        }
    }
}
