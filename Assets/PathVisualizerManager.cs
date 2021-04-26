using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer {

    public class PathVisualizerManager : Singleton<PathVisualizerManager> {
        LineRenderer lr;
        public void VisualizePath(List<Vector2Int> path) {
            path.Insert(0, MazeManager.instance.StartPosition);
            lr = gameObject.AddComponent<LineRenderer>();
            lr.positionCount = path.Count;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            for (int i = 0; i < path.Count; ++i) {
                Vector3 start = path[i].MazeToWorld();
                start.y = 0.1f;
                lr.SetPosition(i, start);
            }
        }
    }
}