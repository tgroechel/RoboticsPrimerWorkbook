using UnityEngine;

namespace RoboticsPrimer {
    [RequireComponent(typeof(TBotHighLevelNavPlanner))]
    public class TBotTurnGoTurn : MonoBehaviour {
        [SerializeField]
        Transform goal;
        [SerializeField]
        bool debugState;

        TBotCommon tbc;
        TBotWheelController wheelController;
        TBotNavigationState navState;
        TBotHighLevelNavPlanner hybridNavigationPlanner;

        float angleTolerance = 10, goalDistTolerance = 0.2f;


        private void Awake() {
            tbc = GetComponent<TBotCommon>();
            wheelController = GetComponent<TBotWheelController>();
            navState = GetComponent<TBotNavigationState>();
            hybridNavigationPlanner = GetComponent<TBotHighLevelNavPlanner>();
            navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.WAITINGFORNAVGOAL;
        }

        private void FixedUpdate() {
            if (debugState) {
                Debug.Log(navState.CurState.ToString());
            }
            switch (navState.CurState) {
                case TBotNavigationState.ROBOT_NAV_STATE.WAITINGFORNAVGOAL:
                    hybridNavigationPlanner.AskForNextGoal();
                    return;
                case TBotNavigationState.ROBOT_NAV_STATE.ATGOAL:
                    navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.WAITINGFORNAVGOAL;
                    break;
                case TBotNavigationState.ROBOT_NAV_STATE.NAVIGATING:
                    SetGoalOnGround();
                    UpdateTurnGoTurn();
                    break;
            }
        }

        private void SetGoalOnGround() {
            if (Mathf.Approximately(goal.position.y, 0)) {
                goal.position = new Vector3(goal.position.x, 0, goal.position.z);
            }
        }

        public void UpdateGoalPosition(Vector2 pos) {
            UpdateGoalPosition(new Vector3(pos.x, 0, pos.y));
            navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.NAVIGATING;
        }

        public void UpdateGoalPosition(Vector3 pos) {
            if (goal != null) {
                goal.position = pos;
                navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.NAVIGATING;
            }
        }

        private void UpdateTurnGoTurn() {
            if (Vector3.Distance(goal.position, tbc.Position) < goalDistTolerance) {
                navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.ATGOAL;

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
