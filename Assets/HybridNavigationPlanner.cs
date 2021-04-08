using UnityEngine;

namespace RoboticsPrimer {
    public class HybridNavigationPlanner : MonoBehaviour {

        public TBotCommon robot;
        TBotTurnGoTurn botTurnGoTurn;

        public enum ROBOT_NAV_STATE {
            NAVIGATING,
            WAITINGFORNAVGOAL,
            ATGOAL,
            STUCK
        }


        public static readonly int[] S = { 0, 0 };  // Stay
        public static readonly int[] U = { 0, -1 }; // Up
        public static readonly int[] D = { 0, 1 };  // Down
        public static readonly int[] L = { -1, 0 }; // Left
        public static int[] R = { 1, 0 };  // Right

        public int[,][] NavPlan { get; set; } = {
            { D, D, D },
            { R, S, L},
            { U, U, L},
            { U, U, U}
        };

        private void Start() {
            if (robot == null) {
                robot = FindObjectOfType<TBotCommon>();
            }
            botTurnGoTurn = robot.GetComponent<TBotTurnGoTurn>();
            botTurnGoTurn.enabled = true;
        }

        private void Update() {
            Vector2Int robotPos = robot.Position.World2MazeRounded();
            Vector2Int goalPos = robotPos;
            goalPos.x += NavPlan[robotPos.y, robotPos.x][0];
            goalPos.y += NavPlan[robotPos.y, robotPos.x][1];
            botTurnGoTurn.UpdateGoalPosition(goalPos.MazeToWorld());
        }
    }
}