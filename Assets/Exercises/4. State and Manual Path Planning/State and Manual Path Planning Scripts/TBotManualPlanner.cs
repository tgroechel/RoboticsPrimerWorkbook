using UnityEngine;
using static RoboticsPrimer.TBotHighLevelNavPlanner;

namespace RoboticsPrimer
{
    public class TBotManualPlanner : TBotPlanner
    {
        #region MEMBERS
        public int[,][] ManualNavPlan { get; set; }
        #endregion

        #region CODE
        /// <summary>
        /// Creates the manual navigation plan to be used by the tbot
        /// This plan is visualized via the `PathVisualizationManager`
        /// </summary>
        public override void CreateAndVisualizePlan()
        {
            ManualNavPlan = new int[,][] {
                // Example plan for `mazeDataExample.txt`
                // Note this is not correct
                // See `TBotHighLevelNavPlanner.cs` for directions (e.g., R = {0, -1})
                { D, D, D },
                { U, U, U},
                { L, S, R},
                { D, U, D}
                /* SOLUTION
                { D, D, D },
                { R, D, L},
                { U, S, L},
                { U, U, U} 
                END */
            };
            PathVisualizerManager.instance.VisualizeManualPath(ManualNavPlan);
        }

        /// <summary>
        /// Given the robot position, calculates the correct next 
        /// local goal position
        /// This will use the `ManualNavPlan` defined above
        /// The goal is then sent in world coordinates
        /// </summary>
        public override void SendNextGoal()
        {
            Vector2Int robotPos = tbc.Position.World2MazeRounded();
            Vector2Int goalPos = robotPos;
            /* SOLUTION
            goalPos.x += ManualNavPlan[robotPos.x, robotPos.y][0];
            goalPos.y += ManualNavPlan[robotPos.x, robotPos.y][1];
            */
            botTurnGoTurn.UpdateGoalPosition(goalPos.Maze2World());
        }
        #endregion
    }
}
