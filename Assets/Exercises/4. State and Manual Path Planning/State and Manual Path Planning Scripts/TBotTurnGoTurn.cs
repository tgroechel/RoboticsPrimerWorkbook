using UnityEngine;

namespace RoboticsPrimer
{
    [RequireComponent(typeof(TBotHighLevelNavPlanner))]
    public class TBotTurnGoTurn : MonoBehaviour
    {
        [SerializeField]
        Transform goal;
        [SerializeField]
        bool debugState;

        TBotCommon tbc;
        TBotWheelController wheelController;
        TBotNavigationState navState;
        TBotHighLevelNavPlanner hybridNavigationPlanner;

        float angleTolerance = 10, goalDistTolerance = 0.2f;

        #region ENGINE
        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
            wheelController = GetComponent<TBotWheelController>();
            navState = GetComponent<TBotNavigationState>();
            hybridNavigationPlanner = GetComponent<TBotHighLevelNavPlanner>();
            navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.WAITINGFORNAVGOAL;
        }
        private void FixedUpdate()
        {
            UpdateStateMachine();
        }
        #endregion

        #region CODE

        /// <summary>
        /// Based upon the `navState.CurState`, does the correct action and updates the state machine accordingly
        /// `UpdateTurnGoTurn` is called here
        /// </summary>
        private void UpdateStateMachine()
        {
            if (debugState)
            {
                Debug.Log(navState.CurState.ToString());
            }
            /* SOLUTION
            switch (navState.CurState)
            {
                case TBotNavigationState.ROBOT_NAV_STATE.WAITINGFORNAVGOAL:
                    hybridNavigationPlanner.AskForNextGoal();
                    return;
                case TBotNavigationState.ROBOT_NAV_STATE.ATGOAL:
                    navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.WAITINGFORNAVGOAL;
                    break;
                case TBotNavigationState.ROBOT_NAV_STATE.NAVIGATING:
                    UpdateTurnGoTurn();
                    break;
            }
            END */
        }

        /// <summary>
        /// Updates the TurnGoTurn policy. This policy does the following:
        /// If close enough to the goal (defined by `goalDistTolerance`):
        ///     changes the state to ATGOAL and stops the robot
        /// Else if the robot is not within the angle tolerance relative to the goal (defined by `angleTolerance`):
        ///     turns right or left to minimize the angle to the goal
        /// Else:
        ///     drives forward
        /// </summary>
        private void UpdateTurnGoTurn()
        {
            // Sets the goal back on the ground just in case it is moved vertically
            SetGoalOnGround();
            // Debug line for the forward heading of the tbot
            Debug.DrawLine(tbc.Position, tbc.Position + tbc.Heading, Color.green);
            /* SOLUTION
            if (Vector3.Distance(goal.position, tbc.Position) < goalDistTolerance)
            {
                navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.ATGOAL;
                wheelController.Stop();
                return;
            }
            Vector3 goalDirection = (goal.position - tbc.Position).normalized;
            float angle = Vector3.SignedAngle(goalDirection, tbc.Heading.normalized, Vector3.up);
            if (angle > angleTolerance)
            {
                wheelController.TurnLeft();
            }
            else if (angle < -angleTolerance)
            {
                wheelController.TurnRight();
            }
            else
            {
                wheelController.GoForward();
            }
            END */
        }
        #endregion

        #region HELPERS
        private void SetGoalOnGround()
        {
            if (Mathf.Approximately(goal.position.y, 0))
            {
                goal.position = new Vector3(goal.position.x, 0, goal.position.z);
            }
        }

        public void UpdateGoalPosition(Vector3 pos)
        {
            if (goal != null)
            {
                goal.position = pos;
                navState.CurState = TBotNavigationState.ROBOT_NAV_STATE.NAVIGATING;
            }
        }
        #endregion
    }
}
