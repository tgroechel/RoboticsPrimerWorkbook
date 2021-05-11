using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoboticsPrimer.HybridNavigationPlanner;


namespace RoboticsPrimer {

    [RequireComponent(typeof(LineRenderer))]
    public class PathVisualizerManager : Singleton<PathVisualizerManager> {
        LineRenderer lineRenderer;
        GameObject arrow;
        Vector3 arrowOffset;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            arrow = Resources.Load<GameObject>(ResourcePathConstants.Arrow);
            arrowOffset = arrow.transform.localPosition;
        }


        public void VisualizePath(List<Vector2Int> path) {
            lineRenderer.enabled = true;
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
            lineRenderer.enabled = false;
            Vector2 pos;
            for (int i = 0; i < manualNavPlan.GetLength(0); ++i) {
                for (int j = 0; j < manualNavPlan.GetLength(1); ++j) {
                    if (manualNavPlan[i, j] == S) {
                        continue;
                    }
                    GameObject tmpArrow = Instantiate(arrow, transform);
                    pos.y = i;
                    pos.x = j;
                    tmpArrow.transform.position = pos.MazeToWorld() + arrowOffset;
                    //tmpArrow.transform.rotation = 
                }
            }
        }
    }
}