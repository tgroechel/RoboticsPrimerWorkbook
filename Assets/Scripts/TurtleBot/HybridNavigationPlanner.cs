using System.Collections.Generic;
using UnityEngine;
using static RoboticsPrimer.MazeConstructor;

namespace RoboticsPrimer {
    public class HybridNavigationPlanner : MonoBehaviour {

        TBotCommon tbc;
        TBotTurnGoTurn botTurnGoTurn;
        [SerializeField]
        bool manualPlan;

        // Threading variables
        Vector2Int goalPosition;
        List<Vector2Int> autonomousNavPlan;

        public static readonly int[] S = { 0, 0 };  // Stay
        public static readonly int[] U = { -1, 0 }; // Up
        public static readonly int[] D = { 1, 0 };  // Down
        public static readonly int[] L = { 0, -1 }; // Left
        public static readonly int[] R = { 0, 1 };  // Right

        public int[,][] ManualNavPlan { get; set; } = {
            { D, D, D },
            { R, S, L},
            { U, U, L},
            { U, U, U}
        };

        private void Start() {
            tbc = GetComponent<TBotCommon>();
            botTurnGoTurn = GetComponent<TBotTurnGoTurn>();
            botTurnGoTurn.enabled = true;
            if (!manualPlan) {
                MazeManager.instance.MazeHasBeenGenerated.AddListener(CreateAutonomousPlan);
            }
            else {
                PathVisualizerManager.instance.VisualizeManualPath(ManualNavPlan);
            }
        }

        void CreateAutonomousPlan() {
            int[,] mazeData = MazeManager.instance.MazeData;
            Vector2Int startPos = MazeManager.instance.StartPosition;
            goalPosition = MazeManager.instance.FinalGoalPosition;

            //bfs
            Stack<KeyValuePair<Vector2Int, int[]>> unvistedStack = new Stack<KeyValuePair<Vector2Int, int[]>>();
            Dictionary<Vector2Int, int[]> visitedSet = new Dictionary<Vector2Int, int[]>();
            Vector2Int curPos = startPos;
            int[] dir = new int[] { 0, 0 };
            unvistedStack.Push(new KeyValuePair<Vector2Int, int[]>(startPos, dir));
            while (curPos != goalPosition) {
                curPos = unvistedStack.Peek().Key;
                dir = unvistedStack.Peek().Value;
                unvistedStack.Pop();
                visitedSet[curPos] = dir;
                UpdateUnvisitedStack(U, curPos, mazeData, visitedSet, unvistedStack);
                UpdateUnvisitedStack(D, curPos, mazeData, visitedSet, unvistedStack);
                UpdateUnvisitedStack(L, curPos, mazeData, visitedSet, unvistedStack);
                UpdateUnvisitedStack(R, curPos, mazeData, visitedSet, unvistedStack);
            }

            if (curPos == goalPosition) {
                BackTrack(visitedSet, curPos, startPos);
            }
            PathVisualizerManager.instance.VisualizePath(autonomousNavPlan);
        }

        private void BackTrack(Dictionary<Vector2Int, int[]> visitedSet, Vector2Int curPos, Vector2Int startPos) {
            autonomousNavPlan = new List<Vector2Int>();
            while (curPos != startPos) {
                autonomousNavPlan.Insert(0, curPos);
                Vector2Int lastPos = curPos;
                curPos.x -= visitedSet[lastPos][0];
                curPos.y -= visitedSet[lastPos][1];
            }
        }

        private void UpdateUnvisitedStack(int[] direction, Vector2Int curPos, int[,] mazeData, Dictionary<Vector2Int, int[]> visistedSet, Stack<KeyValuePair<Vector2Int, int[]>> unvistedStack) {
            curPos = curPos.AddArr(direction);
            if (visistedSet.ContainsKey(curPos)) {
                return;
            }
            if (curPos.x < 0 || curPos.x >= mazeData.GetLength(0) || curPos.y < 0 || curPos.y >= mazeData.GetLength(1)) {
                return;
            }
            if (mazeData[curPos.x, curPos.y] != (int)CELL.O) {
                return;
            }
            unvistedStack.Push(new KeyValuePair<Vector2Int, int[]>(curPos, direction));
        }


        public void AskForNextGoal() {
            if (manualPlan) {
                SendNextManualNavGoal();
            }
            else {
                SendNextAutonomousGoal();
            }
        }

        private void SendNextAutonomousGoal() {
            if (autonomousNavPlan.Count != 0) {
                botTurnGoTurn.UpdateGoalPosition(autonomousNavPlan[0].MazeToWorld());
                autonomousNavPlan.RemoveAt(0);
            }
        }

        private void SendNextManualNavGoal() {
            Vector2Int robotPos = tbc.Position.World2MazeRounded();
            Vector2Int goalPos = robotPos;
            goalPos.x += ManualNavPlan[robotPos.x, robotPos.y][0];
            goalPos.y += ManualNavPlan[robotPos.x, robotPos.y][1];
            botTurnGoTurn.UpdateGoalPosition(goalPos.MazeToWorld());
        }
    }
}