using System.Collections.Generic;
using UnityEngine;
using static RoboticsPrimer.MazeConstructor;
using static RoboticsPrimer.TBotHighLevelNavPlanner;

namespace RoboticsPrimer {
    public class TBotAutonomousPlanner : TBotPlanner {
        Vector2Int goalPosition;
        List<Vector2Int> autonomousNavPlan;

        public override void CreateAndVisualizePlan() {
            int[,] mazeData = MazeManager.instance.MazeData;
            Vector2Int startPos = MazeManager.instance.StartPosition;
            goalPosition = MazeManager.instance.FinalGoalPosition;

            //dfs
            Stack<KeyValuePair<Vector2Int, int[]>> unvistedStack = new Stack<KeyValuePair<Vector2Int, int[]>>();
            Dictionary<Vector2Int, int[]> visitedSet = new Dictionary<Vector2Int, int[]>();
            Vector2Int curPos = startPos;
            int[] dir = new int[] { 0, 0 };
            unvistedStack.Push(new KeyValuePair<Vector2Int, int[]>(startPos, dir));
            while (curPos != goalPosition && unvistedStack.Count != 0) {
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

        private void UpdateUnvisitedStack(int[] direction, Vector2Int curPos, int[,] mazeData, Dictionary<Vector2Int, int[]> visistedSet, Stack<KeyValuePair<Vector2Int, int[]>> unvistedStack) {
            curPos = curPos.AddArr(direction);
            if (visistedSet.ContainsKey(curPos)) {
                return;
            }
            if (curPos.x < 0 || curPos.x >= mazeData.GetLength(0) || curPos.y < 0 || curPos.y >= mazeData.GetLength(1)) {
                return;
            }
            if (mazeData[curPos.x, curPos.y] == (int)CELL.W) {
                return;
            }
            unvistedStack.Push(new KeyValuePair<Vector2Int, int[]>(curPos, direction));
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


        public override void SendNextGoal() {
            if (autonomousNavPlan != null && autonomousNavPlan.Count != 0) {
                botTurnGoTurn.UpdateGoalPosition(autonomousNavPlan[0].Maze2World());
                autonomousNavPlan.RemoveAt(0);
            }
        }
    }
}
