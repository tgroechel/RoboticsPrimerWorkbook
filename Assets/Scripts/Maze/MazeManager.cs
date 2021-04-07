using UnityEngine;

namespace RoboticsPrimer {
    public class MazeManager : Singleton<MazeManager> {
        MazeConstructor mazeConstructor;
        public int[,] MazeData { get; set; } =   {
            {0, 1, 1},
            {0, 3, 1},
            {0, 0, 2},
            {0, 1, 0}
        };

        public GameObject Goal {
            get {
                return mazeConstructor.goal;
            }
        }

        void Start() {
            mazeConstructor = GetComponent<MazeConstructor>();
            mazeConstructor.GenerateNewMaze(MazeData);
        }

        public Vector2 GetMazeXYPosition(Vector3 position) {
            return new Vector2(position.x, -position.z);
        }
        public Vector2 GetMazeXYPositionRounded(Vector3 position) {
            Vector2 tmp = GetMazeXYPosition(position);
            tmp.x = Mathf.Round(tmp.x);
            tmp.y = Mathf.Round(tmp.y);
            return tmp;
        }
    }
}