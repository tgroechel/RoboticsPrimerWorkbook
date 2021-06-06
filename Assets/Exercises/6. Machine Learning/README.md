# Exercise 6: Machine Learning

![MachineLearning](../../../.images/KMeans.png)

## Overview
In this exercise you will learn how to write an unsupervised learning algorithm: [*k*-means clustering](https://en.wikipedia.org/wiki/K-means_clustering). 

## Code Explained

You will be writing code within `Machine Learning Scripts/KMeans.cs`. 

The two files of note are `PointsManager.cs` and `KMeans.cs` both attached to the PointsManager game object found in the heirarchy.

- `PointsManager.cs` : Manages the spawning of points. This has many public inspector variables worth looking at. No code needs to be written but you will use members of this in `KMeans.cs`
- `KMeans.cs`: All of your code will be written here. 


All functions that need coding are marking with `CODE` and found within the `#region CODE`.

## Coding Order and Tips
- If all the comments and extra funcitons are distracting, look into your Editor's ability to do "code folding". An example of this feature can be found [here](https://code.visualstudio.com/docs/editor/codebasics#:~:text=Use%20Shift%20%2B%20Click%20on%20the,uncollapsed%20region%20at%20the%20cursor.).
- `UpdateClustering()` is called each time the respective button is pressed. What order should each of the steps be in? This is where you should start even though nothing will change as you will need to implement the rest of the functions
- `AssignPointsToClusters()` will loook through all of the points in `PointsManager` and assign them to `ClusterGroups`. The assignment will be based on which `ClusterMean` is the closest in distance. [`Vector3.Distance`](https://docs.unity3d.com/ScriptReference/Vector3.Distance.html) will be helpful.
- `UpdateClusterMeans()` will update each mean based on the average position of each point assigned to it.
- With the three above functions implemented, you should be able to generate clusters with different *k*s. These means should update and assign points reporting total error. When do you think you should stop updating?
- *k*-means has many different ways to initialize the means. The implemented way in `SetClusterMeanPositions()` assigns the means to a random position (within the graph bounds). Another way to initialize means is to choose a random point for each and set the `ClusterMean` point's position to that. This is what you will implement. What other techniques could you use for point initialization?
- Which *k* is best for the included cluster means? How do you think you would choose *k* for different examples?
