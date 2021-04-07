using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {
    MazeConstructor mazeConstructor;
    void Start() {
        mazeConstructor = GetComponent<MazeConstructor>();
        int[,] data = new int[,]
        {
            {0, 1, 1},
            {0, 3, 1},
            {0, 0, 0},
            {0, 1, 2}
        };
        mazeConstructor.GenerateNewMaze(data);
    }
}
