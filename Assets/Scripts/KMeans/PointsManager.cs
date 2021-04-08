using System;
using UnityEngine;

namespace RoboticsPrimer {
    public class PointsManager : MonoBehaviour {


        public int numPoints = 50;
        public float pointRadius = 0.2f;
        public bool generateFromClusterMeans;
        public Vector3[] clusterMeans;
        public float standardDeviationOfDistanceFromCluster = 1f;
        public bool useNormalDist;

        private Transform[] points;
        public Transform[] Points {
            get {
                if (points == null) {
                    ResetPoints();
                }
                return points;
            }
            set {
                points = value;
            }
        }
        public float XBound { get; } = 4f;
        public float YBound { get; } = 4f;

        private void Awake() {
            ResetPoints();
        }

        public void ResetPoints() {
            if (points == null || numPoints != points.Length) {
                RemoveOldPoints();
                GeneratePoints();
            }
            SetPointColors();
            SetPointPositions();
        }

        private void SetPointColors() {
            foreach (Transform t in Points) {
                t.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
            }
        }

        private void RemoveOldPoints() {
            if (points == null) {
                return;
            }
            for (int i = 0; i < points.Length; ++i) {
                Destroy(points[i].gameObject);
            }
        }

        private void SetPointPositions() {
            if (generateFromClusterMeans) {
                GeneratePositionsFromClusterMeans();
            }
            else {
                GenerateRandomPositions();
            }
        }

        private void GeneratePositionsFromClusterMeans() {
            int numClusters = clusterMeans.Length;
            standardDeviationOfDistanceFromCluster = Math.Abs(standardDeviationOfDistanceFromCluster);
            CheckClusterParams(numClusters);
            int stepSize = numPoints / numClusters;
            for (int i = 0; i < numClusters; ++i) {
                for (int j = 0; j < stepSize; ++j) {
                    Points[i * stepSize + j].localPosition = GenerateRandomPositionFromMean(clusterMeans[i]);
                }
            }
        }

        private Vector3 GenerateRandomPositionFromMean(Vector3 mean) {
            if (useNormalDist) {
                return new Vector3(ExtensionMethods.NextGaussian(mean.x, standardDeviationOfDistanceFromCluster),
                    ExtensionMethods.NextGaussian(mean.y, standardDeviationOfDistanceFromCluster),
                    0);
            }
            return new Vector3(
                 UnityEngine.Random.Range(mean.x - standardDeviationOfDistanceFromCluster, mean.x + standardDeviationOfDistanceFromCluster),
                UnityEngine.Random.Range(mean.y - standardDeviationOfDistanceFromCluster, mean.y + standardDeviationOfDistanceFromCluster),
                 0);
        }


        private void CheckClusterParams(int numClusters) {
            if (numClusters < 2) {
                Debug.LogError("clusterMeans Array must be at least length 2, please set this in the Inspector view under PointsManager.");
            }
            else if (numClusters >= numPoints) {
                Debug.LogError("clusterMeans Array must be < numPoints, please set this in the Inspector view under PointsManager.");
            }
            if (standardDeviationOfDistanceFromCluster > XBound || standardDeviationOfDistanceFromCluster > YBound) {
                Debug.LogError("standardDeviationOfDistanceFromCluster very large, please make smaller within X " + XBound + "and Y" + YBound + "bounds");
            }
        }

        private void GenerateRandomPositions() {
            foreach (Transform t in Points) {
                t.localPosition = GenerateRandomPosition();
            }
        }

        private void GeneratePoints() {
            points = new Transform[numPoints];
            Vector3 scaler = new Vector3(pointRadius, pointRadius, pointRadius);
            for (int i = 0; i < numPoints; ++i) {
                Points[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                Points[i].SetParent(transform);
                Points[i].localScale = scaler;
            }
        }

        public Vector3 GenerateRandomPosition() {
            return new Vector3(UnityEngine.Random.Range(-XBound, XBound), UnityEngine.Random.Range(-YBound, YBound), 0);
        }
    }
}
