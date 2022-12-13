using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

namespace RoboticsPrimer
{
    public class MazeManager : Singleton<MazeManager>
    {
        [SerializeField]
        bool useMazeFile = true;
        [SerializeField]
        string mazeDataFile = "mazeDataExample.txt";
        [SerializeField]
        int numCols = 5, numRows = 5;


        string mazeDataFilePath = "Assets/MazeData/";
        MazeConstructor mazeConstructor;
        public MazeConstructor MazeConstructor
        {
            get
            {
                if (mazeConstructor == null)
                {
                    Init();
                    mazeConstructor = GetComponent<MazeConstructor>();
                }
                return mazeConstructor;
            }
        }

        UnityEvent mazeHasBeenGenerated;
        public UnityEvent MazeHasBeenGenerated
        {
            get
            {
                if (mazeHasBeenGenerated == null)
                {
                    Init();
                    mazeHasBeenGenerated = new UnityEvent();
                }
                return mazeHasBeenGenerated;
            }
        }

        public int[,] MazeData { get; set; } =   {
            {0, 1, 1},
            {0, 3, 1},
            {0, 0, 0},
            {2, 1, 0}
        };

        public GameObject ImmediateGoal
        {
            get
            {
                return MazeConstructor.goal;
            }
        }

        public Vector2Int FinalGoalPosition
        {
            get
            {
                return MazeConstructor.FinalGoalPos;
            }
        }

        public bool MazeIsGenerated
        {
            get
            {
                return MazeConstructor.MazeIsGenerated;
            }
        }

        public Vector2Int StartPosition
        {
            get
            {
                return MazeConstructor.StartPosVec;
            }
        }

        private void ReadMazeDataFile()
        {
            string[] lines = File.ReadAllLines(mazeDataFilePath + mazeDataFile);
            MazeData = new int[lines.Length, lines[0].Split(' ').Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(' ');
                for (int j = 0; j < cells.Length; j++)
                {
                    int flippedJ = cells.Length - j - 1;
                    MazeData[i, flippedJ] = int.Parse(cells[j]);
                }
            }
        }
        private void Start()
        {
            Init();
        }

        bool hasBeenGenerated = false;
        void Init()
        {
            if (hasBeenGenerated) { return; }
            hasBeenGenerated = true;

            if (useMazeFile)
            {
                ReadMazeDataFile();
                MazeConstructor.GenerateNewMaze(MazeData);
            }
            else
            {
                MazeData = MazeConstructor.GenerateNewMaze(numRows, numCols);
            }
            StartCoroutine(SayMazeReadyAfterACoupleOfFrames());
        }

        IEnumerator SayMazeReadyAfterACoupleOfFrames()
        {
            yield return null;
            yield return null;
            MazeHasBeenGenerated.Invoke();
        }

        internal Vector3 GetWorldPositionFromMazePosition(Vector2 mazePosition)
        {
            return MazeConstructor.GetWorldPositionFromMazePosition(mazePosition);
        }
    }
}