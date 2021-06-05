using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace RoboticsPrimer
{
    public static class ExtensionMethods
    {
        public static Vector2 World2Maze(this Vector3 v)
        {
            return new Vector2(-v.z, v.x);
        }
        public static Vector2 World2Maze(this Vector3Int v)
        {
            return new Vector2(-v.z, v.x);
        }
        public static Vector2Int World2MazeRounded(this Vector3 v)
        {
            Vector2 tmp = v.World2Maze();
            return new Vector2Int(Mathf.RoundToInt(tmp.x), Mathf.RoundToInt(tmp.y));
        }
        public static Vector2Int World2MazeRounded(this Vector3Int v)
        {
            Vector2 tmp = v.World2Maze();
            return new Vector2Int(Mathf.RoundToInt(tmp.x), Mathf.RoundToInt(tmp.y));
        }
        public static Vector3 Maze2World(this Vector2Int v)
        {
            return new Vector3(v.y, 0, -v.x);
        }
        public static Vector3 Maze2World(this Vector2 v)
        {
            return new Vector3(v.y, 0, -v.x);
        }
        public static Vector3Int Maze2WorldRounded(this Vector2 v)
        {
            Vector3 tmp = v.Maze2World();
            return new Vector3Int(Mathf.RoundToInt(tmp.x), 0, Mathf.RoundToInt(tmp.z));
        }
        public static Vector3Int Maze2WorldRounded(this Vector2Int v)
        {
            Vector3 tmp = v.Maze2World();
            return new Vector3Int(Mathf.RoundToInt(tmp.x), 0, Mathf.RoundToInt(tmp.z));
        }

        public static Vector2 AddArr(this Vector2 v, int[] arr)
        {
            Assert.IsTrue(arr.Length == 2);
            v.x += arr[0];
            v.y += arr[1];
            return v;
        }
        public static Vector2Int AddArr(this Vector2Int v, int[] arr)
        {
            Assert.IsTrue(arr.Length == 2);
            v.x += arr[0];
            v.y += arr[1];
            return v;
        }

        public static int[] Add(int[] a, int[] b)
        {
            Assert.IsTrue(a.Length == b.Length);
            for (int i = 0; i < a.Length; ++i)
            {
                a[i] += b[i];
            }
            return a;
        }



        public static float NextGaussian()
        {
            float v1, v2, s;
            do
            {
                v1 = 2.0f * UnityEngine.Random.Range(0f, 1f) - 1.0f;
                v2 = 2.0f * UnityEngine.Random.Range(0f, 1f) - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0f);

            s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

            return v1 * s;
        }

        public static float NextGaussian(float mean, float standard_deviation)
        {
            return mean + NextGaussian() * standard_deviation;
        }


    }
}
