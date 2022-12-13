using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class EndCubeIndicator : MonoBehaviour
    {
        private void Start()
        {
            MazeManager.instance.MazeHasBeenGenerated.AddListener(SpawnCubeAtEnd);
        }
        void Update()
        {
            // make rotate around the gloab up axis
            transform.Rotate(Vector3.up, 20 * Time.deltaTime, Space.World);
        }

        void SpawnCubeAtEnd()
        {
            Vector2 endMazePosition = MazeManager.instance.FinalGoalPosition;
            Vector3 endWorldPosition = MazeManager.instance.GetWorldPositionFromMazePosition(endMazePosition);
            endWorldPosition.y = 0.5f;
            transform.position = endWorldPosition;
        }
    }
}
