using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer {

    [RequireComponent(typeof(LineRenderer))]
    public class PathVisualizerManager : Singleton<PathVisualizerManager> {
        LineRenderer lineRenderer;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            GameObject arrow = Resources.Load<GameObject>(ResourcePathConstants.Arrow);
            Instantiate(arrow);
        }


        public void VisualizePath(List<Vector2Int> path) {
            path.Insert(0, MazeManager.instance.StartPosition);
            lineRenderer.positionCount = path.Count;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            for (int i = 0; i < path.Count; ++i) {
                Vector3 start = path[i].MazeToWorld();
                start.y = 0.1f;
                lineRenderer.SetPosition(i, start);
            }
        }

        public void VisualizeManualPath(int[,][] manualNavPlan) {
            // foreach (int[,] dir in manualNavPlan) {

            //  }
        }
    }
}