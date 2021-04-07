using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {
    MazeConstructor mazeConstructor;
    // Start is called before the first frame update
    void Start() {
        mazeConstructor = GetComponent<MazeConstructor>();
        int[,] data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
        mazeConstructor.GenerateNewMaze(data);
    }
}
