using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoboticsPrimer.TBotHighLevelNavPlanner;


namespace RoboticsPrimer {

    [RequireComponent(typeof(LineRenderer))]
    public class PathVisualizerManager : Singleton<PathVisualizerManager> {
        LineRenderer lineRenderer;
        GameObject arrow;
        Dictionary<int[], Quaternion> arrowDirectionMap = new Dictionary<int[], Quaternion> {
            { U , Quaternion.Euler(90f,0f,0f) },
            { D , Quaternion.Euler(270f,0f,0f) },
            { L , Quaternion.Euler(270f,90f,0f) },
            { R , Quaternion.Euler(270f,270f,0f) }
        };

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            arrow = Resources.Load<GameObject>(ResourcePathConstants.Arrow);
        }

        public void VisualizePath(List<Vector2Int> path) {
            lineRenderer.enabled = true;
            path.Insert(0, MazeManager.instance.StartPosition);
            lineRenderer.positionCount = path.Count;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            for (int i = 0; i < path.Count; ++i) {
                Vector3 start = path[i].Maze2World();
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
                    pos.x = i;
                    pos.y = j;
                    tmpArrow.transform.position = pos.Maze2World();
                    int[] dir = manualNavPlan[i, j];
                    Vector3 lookDir = (new Vector2(dir[0], dir[1])).Maze2World();
                    tmpArrow.transform.localRotation = Quaternion.LookRotation(lookDir, transform.forward);
                    tmpArrow.transform.localRotation = arrowDirectionMap[dir];
                }
            }
        }
    }
}