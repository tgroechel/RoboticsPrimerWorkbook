using UnityEngine;

namespace RoboticsPrimer {
    [RequireComponent(typeof(TBotCommon), typeof(TBotFourWayLaserScanner), typeof(TBotWheelController))]
    public class TBotAvoidObstaclesController : MonoBehaviour {
        TBotCommon tbc;
        TBotFourWayLaserScanner laserScanner;
        TBotWheelController wheelController;
        public Transform goal;
        private void Awake() {
            tbc = GetComponent<TBotCommon>();
            laserScanner = GetComponent<TBotFourWayLaserScanner>();
            wheelController = GetComponent<TBotWheelController>();
            if (goal == null) {
                goal = MazeManager.instance.ImmediateGoal?.transform;
            }
        }

        private void FixedUpdate() {
            // Math https://www.usna.edu/Users/cs/crabbe/SI475/current/mob-kin/mobkin.pdf
            Vector2 wheelDir = Vector2.zero;
            Vector3 goalDir = (goal.position - tbc.Position).normalized + tbc.BaseLink.forward;
            foreach (var scan in laserScanner.Scans) {
                //   goalDir += tbc.Position - scan.point;
            }
            goalDir *= wheelController.maxWheelSpeed;

            wheelController.SendVelocityCommand(new Vector2(goalDir.x, goalDir.z));
            Debug.DrawRay(tbc.Position, new Vector3(goalDir.x, 0, goalDir.z));
            Debug.Log(new Vector2(goalDir.x, goalDir.z));
        }
    }
}
