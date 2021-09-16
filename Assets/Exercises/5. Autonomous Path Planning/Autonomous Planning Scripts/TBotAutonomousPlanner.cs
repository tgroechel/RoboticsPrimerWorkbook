using System.Collections.Generic;
using UnityEngine;
using static RoboticsPrimer.MazeConstructor;
using static RoboticsPrimer.TBotHighLevelNavPlanner;

namespace RoboticsPrimer
{
    public class TBotAutonomousPlanner : TBotPlanner
    {
        #region MEMBERS
        Vector2Int goalPosition;
        List<Vector2Int> autonomousNavPlan = new List<Vector2Int>();
        #endregion

        #region CODE
        /// <summary>
        /// CODE: creates the `List<Vector2Int> autonomousNavPlan` which is a `List` of all subgoal positions
        /// Ex. Start (0,0), End(1,1) -> [(0,0),(0,1),(1,1)]
        /// There are many search algorithms you can use to accomplish this, I would reccomend either
        /// Breath First Search (BFS) or Depth First Search (DFS)
        /// You can also take advantage of the directions given in `RoboticsPrimer.TBotHighLevelNavPlanner` of `U`, `D`, `L`, `R`
        /// </summary>
        public override void CreateAndVisualizePlan()
        {
            int[,] mazeData = MazeManager.instance.MazeData;
            Vector2Int startPos = MazeManager.instance.StartPosition;
            goalPosition = MazeManager.instance.FinalGoalPosition;

            /* SOLUTION
            // Breath First Search (BFS)
            // Switch to a `Stack` for Depth First Search (DFS)
            Queue<KeyValuePair<Vector2Int, int[]>> unvistedQueue = new Queue<KeyValuePair<Vector2Int, int[]>>();
            Dictionary<Vector2Int, int[]> visitedSet = new Dictionary<Vector2Int, int[]>();
            Vector2Int curPos = startPos;
            int[] dir = new int[] { 0, 0 };
            unvistedQueue.Enqueue(new KeyValuePair<Vector2Int, int[]>(startPos, dir));
            while (curPos != goalPosition && unvistedQueue.Count != 0)
            {
                curPos = unvistedQueue.Peek().Key;
                dir = unvistedQueue.Peek().Value;
                unvistedQueue.Dequeue();
                visitedSet[curPos] = dir;
                UpdateUnvisited(U, curPos, mazeData, visitedSet, unvistedQueue);
                UpdateUnvisited(D, curPos, mazeData, visitedSet, unvistedQueue);
                UpdateUnvisited(L, curPos, mazeData, visitedSet, unvistedQueue);
                UpdateUnvisited(R, curPos, mazeData, visitedSet, unvistedQueue);
            }

            if (curPos == goalPosition)
            {
                BackTrack(visitedSet, curPos, startPos);
            }
            
            END */
            PathVisualizerManager.instance.VisualizePath(autonomousNavPlan);
        }

        /// <summary>
        /// CODE: This is a suggested helper for your search algorithm by updating unvisted squares given a direction
        /// Don't forget to check for walls using `(int)CELL.W`
        /// </summary>
        /// <param name="direction">Direction of next move</param>
        /// <param name="curPos">Current position in search</param>
        /// <param name="mazeData">2D Maze data</param>
        /// <param name="visistedSet">All visisted positions</param>
        /// <param name="unvisted">All unvisted positions</param>
        private void UpdateUnvisited(int[] direction, Vector2Int curPos, int[,] mazeData, Dictionary<Vector2Int, int[]> visistedSet, Queue<KeyValuePair<Vector2Int, int[]>> unvisted)
        {

            curPos = curPos.AddArr(direction);
            /* SOLUTION
            if (visistedSet.ContainsKey(curPos))
            {
                return;
            }
            if (curPos.x < 0 || curPos.x >= mazeData.GetLength(0) || curPos.y < 0 || curPos.y >= mazeData.GetLength(1))
            {
                return;
            }
            if (mazeData[curPos.x, curPos.y] == (int)CELL.W)
            {
                return;
            }
            unvisted.Enqueue(new KeyValuePair<Vector2Int, int[]>(curPos, direction));
            END */
        }

        /// <summary>
        /// CODE: This is a suggested helper for your search algorithm that creates `autonomousNavPlan` by backtracking
        /// </summary>
        /// <param name="visitedSet"></param>
        /// <param name="curPos"></param>
        /// <param name="startPos"></param>
        private void BackTrack(Dictionary<Vector2Int, int[]> visitedSet, Vector2Int curPos, Vector2Int startPos)
        {
            /* SOLUTION
            while (curPos != startPos)
            {
                autonomousNavPlan.Insert(0, curPos);
                Vector2Int lastPos = curPos;
                curPos.x -= visitedSet[lastPos][0];
                curPos.y -= visitedSet[lastPos][1];
            }
            END */
        }
        #endregion
        #region HELPERS
        /// <summary>
        /// Sends next goal to the low level `TurnGoTurn` planner
        /// </summary>
        public override void SendNextGoal()
        {
            if (autonomousNavPlan != null && autonomousNavPlan.Count != 0)
            {
                botTurnGoTurn.UpdateGoalPosition(autonomousNavPlan[0].Maze2World());
                autonomousNavPlan.RemoveAt(0);
            }
        }
        #endregion
    }
}
