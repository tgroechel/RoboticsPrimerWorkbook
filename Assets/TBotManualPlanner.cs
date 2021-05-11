using System.Collections.Generic;
using UnityEngine;
using static RoboticsPrimer.MazeConstructor;
using static RoboticsPrimer.TBotHighLevelNavPlanner;

namespace RoboticsPrimer {
    public class TBotManualPlanner : TBotPlanner {
        public int[,][] ManualNavPlan { get; set; }

        public override void CreateAndVisualizePlan() {
            ManualNavPlan = new int[,][] {
                { D, D, D },
                { R, S, L},
                { U, U, L},
                { U, U, U}
            };
            PathVisualizerManager.instance.VisualizeManualPath(ManualNavPlan);
        }

        public override void SendNextGoal() {
            Vector2Int robotPos = tbc.Position.World2MazeRounded();
            Vector2Int goalPos = robotPos;
            goalPos.x += ManualNavPlan[robotPos.x, robotPos.y][0];
            goalPos.y += ManualNavPlan[robotPos.x, robotPos.y][1];
            botTurnGoTurn.UpdateGoalPosition(goalPos.Maze2World());
        }
    }
}
