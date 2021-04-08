using UnityEngine;

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

        public int[,] MazeData { get; set; } =   {
            {0, 1, 1},
            {0, 3, 1},
            {0, 0, 0},
            {2, 1, 0}
        };

        public GameObject Goal {
            get {
                return MazeConstructor.goal;
            }
        }

        void Start() {
            MazeConstructor.GenerateNewMaze(MazeData);
        }


    }
}