using UnityEngine;

namespace RoboticsPrimer
{
    [RequireComponent(typeof(TBotCommon), typeof(TBotFourWayLaserScanner), typeof(TBotWheelController))]
    public class TBotAvoidObstaclesController : MonoBehaviour
    {
        TBotCommon tbc;
        TBotFourWayLaserScanner laserScanner;
        TBotWheelController wheelController;
        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
            laserScanner = GetComponent<TBotFourWayLaserScanner>();
            wheelController = GetComponent<TBotWheelController>();
        }

        private void FixedUpdate()
        {
            float minDist = laserScanner.Scans[0].distance;
            int ind = 0;
            for (int i = 1; i < 4; ++i)
            {
                if (laserScanner.Scans[i].distance < minDist)
                {
                    minDist = laserScanner.Scans[i].distance;
                    ind = i;
                }
            }
            Debug.Log(ind);
        }


    }
}
