using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer {
    public class KMeans : MonoBehaviour {
        public int NumClusterK { get; set; } = 4;
        public Transform[] ClusterMeans { get; set; }
        public List<Transform>[] ClusterGroups { get; set; }
        public Color[] ClusterColors { get; set; }

        PointsManager pointsManager;

        private void Awake() {
            pointsManager = GetComponent<PointsManager>();
            ResetClusters();
        }

        private void ResetClusters() {
            GenerateClusterColors();
            InitializeClusterMeans();
            CreateClusterGroups();
        }

        private void CreateClusterGroups() {
            ClusterGroups = new List<Transform>[NumClusterK];
            for (int i = 0; i < NumClusterK; ++i) {
                ClusterGroups[i] = new List<Transform>();
            }
        }

        private void GenerateClusterColors() {
            ClusterColors = new Color[NumClusterK];
            for (int i = 0; i < NumClusterK; ++i) {
                ClusterColors[i] = Color.HSVToRGB((float)i / NumClusterK, 1, 1);
            }
        }

        private void InitializeClusterMeans() {
            ClusterMeans = new Transform[NumClusterK];
            Vector3 scaler = new Vector3(pointsManager.pointRadius, pointsManager.pointRadius, pointsManager.pointRadius);
            for (int i = 0; i < NumClusterK; ++i) {
                ClusterMeans[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                ClusterMeans[i].SetParent(transform);
                ClusterMeans[i].localScale = 2 * scaler;
                ClusterMeans[i].localPosition = pointsManager.GenerateRandomPosition();
                ClusterMeans[i].GetComponent<MeshRenderer>().material.SetColor("_Color", ClusterColors[i]);
            }
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha0)) {
                UpdateClustering();
            }
        }

        private void UpdateClustering() {
            ResetClusterGroups();
            AssignPointsToClusters();
            UpdateAssignedPointColors();
            UpdateClusterMeans();
        }

        private void ResetClusterGroups() {
            for (int i = 0; i < NumClusterK; ++i) {
                ClusterGroups[i].Clear();
            }
        }

        private void AssignPointsToClusters() {
            foreach (Transform t in pointsManager.Points) {
                float bestDist = pointsManager.XBound * pointsManager.YBound;
                int bestInd = -1;
                for (int i = 0; i < NumClusterK; ++i) {
                    float dist = Vector3.Distance(t.position, ClusterMeans[i].position);
                    if (dist < bestDist) {
                        bestDist = dist;
                        bestInd = i;
                    }
                }
                ClusterGroups[bestInd].Add(t);
            }
        }

        private void UpdateAssignedPointColors() {
            for (int i = 0; i < NumClusterK; ++i) {
                foreach (Transform t in ClusterGroups[i]) {
                    t.GetComponent<MeshRenderer>().material.SetColor("_Color", ClusterColors[i]);
                }
            }
        }

        private void UpdateClusterMeans() {
            for (int i = 0; i < NumClusterK; ++i) {
                Vector3 averagePosition = Vector3.zero;
                foreach (Transform t in ClusterGroups[i]) {
                    averagePosition += t.localPosition;
                }
                averagePosition /= ClusterGroups[i].Count;
                ClusterMeans[i].localPosition = averagePosition;
            }
        }
    }
}
