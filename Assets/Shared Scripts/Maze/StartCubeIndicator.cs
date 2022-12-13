using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class StartCubeIndicator : MonoBehaviour
    {
        private void Start()
        {
            MazeManager.instance.MazeHasBeenGenerated.AddListener(SpawnCubeAtStart);
        }
        void Update()
        {
            // make rotate around the gloab up axis
            transform.Rotate(Vector3.up, 20 * Time.deltaTime, Space.World);
        }

        void SpawnCubeAtStart()
        {
            Vector2 startMazePosition = MazeManager.instance.StartPosition;
            Vector3 startWorldPosition = MazeManager.instance.GetWorldPositionFromMazePosition(startMazePosition);
            startWorldPosition.y = 0.5f;
            transform.position = startWorldPosition;
        }
    }
}
