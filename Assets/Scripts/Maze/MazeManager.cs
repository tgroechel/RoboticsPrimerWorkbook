using UnityEngine;
using UnityEngine.Events;

namespace RoboticsPrimer {
    public class MazeManager : Singleton<MazeManager> {
        MazeConstructor mazeConstructor;
        public MazeConstructor MazeConstructor {
            get {
                if (mazeConstructor == null) {
                    mazeConstructor = GetComponent<MazeConstructor>();
                }
                return mazeConstructor;
            }
        }

        UnityEvent mazeHasBeenGenerated;
        public UnityEvent MazeHasBeenGenerated {
            get {
                if (mazeHasBeenGenerated == null) {
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

        public GameObject ImmediateGoal {
            get {
                return MazeConstructor.goal;
            }
        }

        public Vector2Int FinalGoalPosition {
            get {
                return MazeConstructor.FinalGoalPos;
            }
        }

        public bool MazeIsGenerated {
            get {
                return MazeConstructor.MazeIsGenerated;
            }
        }

        public Vector2Int StartPosition {
            get {
                return MazeConstructor.StartPosVec;
            }
        }

        void Start() {
            MazeConstructor.GenerateNewMaze(MazeData);
        }


    }
}