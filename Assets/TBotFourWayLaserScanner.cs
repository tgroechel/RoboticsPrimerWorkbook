using UnityEngine;

namespace RoboticsPrimer
{
    [RequireComponent(typeof(TBotLaserScanner))]
    public class TBotFourWayLaserScanner : MonoBehaviour
    {
        [SerializeField]
        float maxLaserRange;

        TBotLaserScanner laserScanner;
        private void Awake()
        {
            laserScanner = GetComponent<TBotLaserScanner>();
            laserScanner.NumLaserScans = 4;
            laserScanner.MaxLaserRange = maxLaserRange;
        }

        public RaycastHit[] Scans
        {
            get
            {
                return laserScanner.Scans;
            }
        }

        public RaycastHit LaserFront
        {
            get
            {
                return Scans[0];
            }
        }
        public RaycastHit LaserRight
        {
            get
            {
                return Scans[1];
            }
        }
        public RaycastHit LaserBack
        {
            get
            {
                return Scans[2];
            }
        }
        public RaycastHit LaserLeft
        {
            get
            {
                return Scans[3];
            }
        }
    }
}