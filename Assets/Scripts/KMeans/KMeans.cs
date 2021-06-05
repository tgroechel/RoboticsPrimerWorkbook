using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class KMeans : MonoBehaviour
    {
        public bool usePointInitialization;
        public int NumClusterK { get; set; } = 4;
        public Transform[] ClusterMeans { get; set; }
        public List<Transform>[] ClusterGroups { get; set; }
        public Color[] ClusterColors { get; set; }

        PointsManager pointsManager;
        KTextUI kTextUI;

        private void Awake()
        {
            pointsManager = GetComponent<PointsManager>();
            kTextUI = FindObjectOfType<KTextUI>();
            Reset();
        }

        public void Reset()
        {
            RemovePriorClusterMeans();
            GenerateClusterColors();
            InitializeClusterMeans();
            SetClusterMeanPositions();
            CreateClusterGroups();
            kTextUI.UpdateText(NumClusterK);
        }

        private void SetClusterMeanPositions()
        {
            foreach (Transform t in ClusterMeans)
            {
                if (usePointInitialization)
                {
                    t.localPosition = pointsManager.Points[UnityEngine.Random.Range(0, pointsManager.numPoints)].localPosition;
                }
                else
                {
                    t.localPosition = pointsManager.GenerateRandomPosition();
                }
            }
        }

        private void RemovePriorClusterMeans()
        {
            if (ClusterMeans == null)
            {
                return;
            }
            for (int i = 0; i < ClusterMeans.Length; ++i)
            {
                Destroy(ClusterMeans[i].gameObject);
            }
        }

        private void CreateClusterGroups()
        {
            ClusterGroups = new List<Transform>[NumClusterK];
            for (int i = 0; i < NumClusterK; ++i)
            {
                ClusterGroups[i] = new List<Transform>();
            }
        }

        private void GenerateClusterColors()
        {
            ClusterColors = new Color[NumClusterK];
            for (int i = 0; i < NumClusterK; ++i)
            {
                ClusterColors[i] = Color.HSVToRGB((float)i / NumClusterK, 1, 1);
            }
        }

        private void InitializeClusterMeans()
        {
            ClusterMeans = new Transform[NumClusterK];
            Vector3 scaler = new Vector3(pointsManager.pointRadius, pointsManager.pointRadius, pointsManager.pointRadius);
            for (int i = 0; i < NumClusterK; ++i)
            {
                ClusterMeans[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                ClusterMeans[i].SetParent(transform);
                ClusterMeans[i].localScale = 2 * scaler;
                ClusterMeans[i].GetComponent<MeshRenderer>().material.SetColor("_Color", ClusterColors[i]);
            }
        }

        private void ResetClusterGroups()
        {
            for (int i = 0; i < NumClusterK; ++i)
            {
                ClusterGroups[i].Clear();
            }
        }

        public void ChangePoints()
        {
            pointsManager.ResetPoints();
            Reset();
        }

        public void SubFromK()
        {
            NumClusterK -= 1;
            if (NumClusterK < 2)
            {
                NumClusterK = 2;
            }
            Reset();
        }

        public void AddToK()
        {
            NumClusterK += 1;
            Reset();
        }

        public void UpdateClustering()
        {
            ResetClusterGroups();
            AssignPointsToClusters();
            UpdateAssignedPointColors();
            UpdateClusterMeans();
        }


        private void AssignPointsToClusters()
        {
            float totalError = 0;
            foreach (Transform t in pointsManager.Points)
            {
                float bestDist = pointsManager.XBound * pointsManager.YBound;
                int bestInd = -1;
                for (int i = 0; i < NumClusterK; ++i)
                {
                    float dist = Vector3.Distance(t.position, ClusterMeans[i].position);
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        bestInd = i;
                    }
                }
                ClusterGroups[bestInd].Add(t);
                totalError += bestDist;
            }
            Debug.Log("K = " + NumClusterK + "Total Error: " + totalError);
        }

        private void UpdateAssignedPointColors()
        {
            for (int i = 0; i < NumClusterK; ++i)
            {
                foreach (Transform t in ClusterGroups[i])
                {
                    t.GetComponent<MeshRenderer>().material.SetColor("_Color", ClusterColors[i]);
                }
            }
        }

        private void UpdateClusterMeans()
        {
            for (int i = 0; i < NumClusterK; ++i)
            {
                if (ClusterGroups[i].Count == 0)
                {
                    continue;
                }
                Vector3 averagePosition = Vector3.zero;
                foreach (Transform t in ClusterGroups[i])
                {
                    averagePosition += t.localPosition;
                }
                averagePosition /= ClusterGroups[i].Count;
                ClusterMeans[i].localPosition = averagePosition;
            }
        }
    }
}
