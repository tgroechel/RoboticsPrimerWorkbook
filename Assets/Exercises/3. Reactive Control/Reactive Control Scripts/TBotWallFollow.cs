using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotWallFollow : MonoBehaviour
    {
        #region MEMBERS
        TBotCommon tbc;
        TBotWheelController wheelController;
        TBotFourWayLaserScanner fourWayLaser;
        float distToWallThreshold = 0.2f, goalDistThreshold = 0.5f;
        Vector3 goalLocation;
        #endregion
        #region ENGINE
        void Awake()
        {
            tbc = GetComponent<TBotCommon>();
            wheelController = GetComponent<TBotWheelController>();
            fourWayLaser = GetComponent<TBotFourWayLaserScanner>();
            goalLocation = MazeManager.instance.GetWorldPositionFromMazePosition(MazeManager.instance.FinalGoalPosition);
        }

        void FixedUpdate()
        {
            UpdateWallFollow();
        }
        #endregion
        #region Code
        void UpdateWallFollow()
        {
            // follow the left wall until there is an object right in front of us
            // drive forward until there is a wall right in front of us
            // turn right until there is no longer a wall right in front of us
            // drive forward until there is a wall right in front of us
            // turn left until there is no longer a wall right in front of us
            // repeat
            // SOLUTION
            // calculate dist to Goal
            float distToGoal = Vector3.Distance(tbc.transform.position, goalLocation);
            // if we are close enough to the goal, stop
            if (distToGoal < goalDistThreshold)
            {
                wheelController.Stop();
                return;
            }
            float distToWallInFront = fourWayLaser.LaserFront.distance;
            Debug.Log(distToWallInFront);
            if (distToWallInFront > distToWallThreshold)
            {
                // drive forward
                wheelController.GoForward();
            }
            else
            {
                // turn right
                wheelController.TurnRight();
            }

        }
        #endregion

    }
}
