using UnityEngine;

namespace RoboticsPrimer {
    public class PointsManager : MonoBehaviour {

        public int numPoints = 50;
        public float pointRadius = 0.2f;
        public Transform[] Points { get; set; }
        public float XBound { get; } = 3f;
        public float YBound { get; } = 3f;

        void Awake() {
            GeneratePoints();
        }

        private void GeneratePoints() {
            Points = new Transform[numPoints];
            Vector3 scaler = new Vector3(pointRadius, pointRadius, pointRadius);
            for (int i = 0; i < numPoints; ++i) {
                Points[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                Points[i].SetParent(transform);
                Points[i].localScale = scaler;
                Points[i].localPosition = GenerateRandomPosition();
            }
        }

        public Vector3 GenerateRandomPosition() {
            return new Vector3(UnityEngine.Random.Range(-XBound, XBound), UnityEngine.Random.Range(-YBound, YBound), 0);
        }
    }
}
