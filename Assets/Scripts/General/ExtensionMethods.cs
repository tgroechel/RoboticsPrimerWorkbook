using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer {
    public static class ExtensionMethods {
        public static Vector2 World2Maze(this Vector3 v) {
            return new Vector2(v.x, -v.z);
        }
        public static Vector2 World2Maze(this Vector3Int v) {
            return new Vector2(v.x, -v.z);
        }
        public static Vector2Int World2MazeRounded(this Vector3 v) {
            Vector2 tmp = v.World2Maze();
            return new Vector2Int(Mathf.RoundToInt(tmp.x), Mathf.RoundToInt(tmp.y));
        }
        public static Vector2Int World2MazeRounded(this Vector3Int v) {
            Vector2 tmp = v.World2Maze();
            return new Vector2Int(Mathf.RoundToInt(tmp.x), Mathf.RoundToInt(tmp.y));
        }

        public static Vector3 MazeToWorld(this Vector2Int v) {
            return new Vector3(v.x, 0, -v.y);
        }
        public static Vector3 MazeToWorld(this Vector2 v) {
            return new Vector3(v.x, 0, -v.y);
        }
        public static Vector3Int MazeToWorldRounded(this Vector2 v) {
            Vector3 tmp = v.MazeToWorld();
            return new Vector3Int(Mathf.RoundToInt(tmp.x), 0, Mathf.RoundToInt(tmp.z));
        }
        public static Vector3Int MazeToWorldRounded(this Vector2Int v) {
            Vector3 tmp = v.MazeToWorld();
            return new Vector3Int(Mathf.RoundToInt(tmp.x), 0, Mathf.RoundToInt(tmp.z));
        }



    }
}
