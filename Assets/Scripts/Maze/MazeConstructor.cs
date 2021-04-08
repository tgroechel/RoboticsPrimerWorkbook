using System;
using UnityEngine;

namespace RoboticsPrimer {
    public class MazeConstructor : MonoBehaviour {

        public bool showDebug;
        private MazeDataGenerator dataGenerator;
        private MazeMeshGenerator meshGenerator;

        public enum CELL {
            O = 0,
            W = 1,
            S = 2,
            G = 3
        }

        public float HallWidth {
            get; private set;
        }
        public float HallHeight {
            get; private set;
        }

        public int StartRow {
            get; private set;
        }
        public int StartCol {
            get; private set;
        }

        public int GoalRow {
            get; private set;
        }
        public int GoalCol {
            get; private set;
        }

        public Vector2 StartPosVec {
            get {
                return new Vector2(StartCol, StartRow);
            }
        }
        public Vector2 DataDim {
            get {
                return new Vector2(data.GetUpperBound(0) + 1, data.GetUpperBound(1) + 1);
            }
        }

        [SerializeField] private Material mazeMat1;
        [SerializeField] private Material mazeMat2;
        [SerializeField] private Material startMat;
        [SerializeField] private Material treasureMat;
        [SerializeField] public GameObject robot;
        [SerializeField] public GameObject goal;

        //2
        public int[,] data {
            get; private set;
        }


        void Awake() {
            // default to walls surrounding a single empty cell
            data = new int[,]
            {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
            };
            dataGenerator = new MazeDataGenerator();
            meshGenerator = new MazeMeshGenerator();
            if (robot == null) {
                robot = FindObjectOfType<TBotCommon>()?.gameObject;
            }
            if (goal == null) {
                goal = GameObject.Find("Goal");
            }
        }

        public void GenerateNewMaze(int[,] _data) {
            data = _data;
            SetUpMazeConstraints();
            ReverseData();
            DisplayMaze();
            MoveRobot();
            MoveGoal();
        }

        private void MoveRobot() {
            if (robot != null) {
                robot.GetComponent<TBotCommon>().GetLink(TBotCommon.base_link).position = new Vector3(StartCol, 0, -StartRow);
            }
        }

        private void MoveGoal() {
            if (goal != null) {
                goal.transform.position = new Vector3(GoalCol, 0, -GoalRow);
            }
        }

        private void SetUpMazeConstraints() {
            FindStartPosition();
            FindGoalPosition();

            // store values used to generate this mesh
            HallWidth = meshGenerator.width;
            HallHeight = meshGenerator.height;
        }

        private void ReverseData() {
            int rMax = data.GetUpperBound(0) + 1;
            int cMax = data.GetUpperBound(1) + 1;
            for (int i = 0; i < rMax / 2; ++i) {
                for (int j = 0; j < cMax; ++j) {
                    int tmp = data[i, j];
                    data[i, j] = data[rMax - i - 1, j];
                    data[rMax - i - 1, j] = tmp;
                }
            }
        }

        public void GenerateNewMaze(int sizeRows, int sizeCols,
        TriggerEventHandler startCallback = null, TriggerEventHandler goalCallback = null) {
            if (sizeRows % 2 == 0 && sizeCols % 2 == 0) {
                Debug.LogError("Odd numbers work better for dungeon size.");
            }

            DisposeOldMaze();

            data = dataGenerator.FromDimensions(sizeRows, sizeCols);

            SetUpMazeConstraints();

            DisplayMaze();

            PlaceStartTrigger(startCallback);
            PlaceGoalTrigger(goalCallback);
        }

        private void DisplayMaze() {
            GameObject go = new GameObject();
            go.transform.position = CalculateZeroStartPosition();
            go.name = "Procedural Maze";

            MeshFilter mf = go.AddComponent<MeshFilter>();
            mf.mesh = meshGenerator.FromData(data);

            MeshCollider mc = go.AddComponent<MeshCollider>();
            mc.sharedMesh = mf.mesh;

            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            mr.materials = new Material[2] { mazeMat1, mazeMat2 };
        }

        private Vector3 CalculateZeroStartPosition() {
            return new Vector3(0, 0, -DataDim.y);
            //return new Vector3(-StartCol, 0, -StartRow);
        }

        private void FindStartPosition() {
            int[,] maze = data;
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            for (int i = 0; i <= rMax; i++) {
                for (int j = 0; j <= cMax; j++) {
                    if (maze[i, j] == (int)CELL.S) {
                        StartRow = i;
                        StartCol = j;
                        return;
                    }
                }
            }
        }

        private void FindGoalPosition() {
            int[,] maze = data;
            int rMax = maze.GetUpperBound(0);
            int cMax = maze.GetUpperBound(1);

            // loop top to bottom, right to left
            for (int i = rMax; i >= 0; i--) {
                for (int j = cMax; j >= 0; j--) {
                    if (maze[i, j] == (int)CELL.G) {
                        GoalRow = i;
                        GoalCol = j;
                        return;
                    }
                }
            }
        }

        private void PlaceStartTrigger(TriggerEventHandler callback) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(StartCol * HallWidth, .5f, StartRow * HallWidth);
            go.name = "Start Trigger";

            go.GetComponent<BoxCollider>().isTrigger = true;
            go.GetComponent<MeshRenderer>().sharedMaterial = startMat;

            TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
            tc.callback = callback;
        }

        private void PlaceGoalTrigger(TriggerEventHandler callback) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(GoalCol * HallWidth, .5f, GoalRow * HallWidth);
            go.name = "Treasure";

            go.GetComponent<BoxCollider>().isTrigger = true;
            go.GetComponent<MeshRenderer>().sharedMaterial = treasureMat;

            TriggerEventRouter tc = go.AddComponent<TriggerEventRouter>();
            tc.callback = callback;
        }



        public void DisposeOldMaze() {
            // GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
            //  foreach (GameObject go in objects) {
            //        Destroy(go);
            //   }
        }
    }
}