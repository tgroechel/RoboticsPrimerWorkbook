using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotLaserScanner : MonoBehaviour
    {
        [field: SerializeField]
        public int NumLaserScans
        {
            get; set;
        }
        [field: SerializeField]
        public int MaxLaserRage
        {
            get; set;
        }

        RaycastHit[] scans;
        public RaycastHit[] Scans
        {
            get
            {
                if (scans == null || scans.Length != NumLaserScans)
                {
                    scans = new RaycastHit[NumLaserScans];
                }
                return scans;
            }
        }

        TBotCommon tbc;


        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void FixedUpdate()
        {
            UpdateLaserScan();
            DrawDebugLaserLines();
        }


        private void UpdateLaserScan()
        {
            float angle = 0;
            for (int i = 0; i < NumLaserScans; i++)
            {
                Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                RaycastHit hit;

                if (!Physics.Raycast(tbc.BaseScanLink.position, dir, out hit, MaxLaserRage))
                {
                    hit.point = tbc.BaseScanLink.position + dir * MaxLaserRage;
                    hit.distance = MaxLaserRage;
                }

                Scans[i] = hit;
                angle += 2 * Mathf.PI / NumLaserScans;
            }
        }

        private void DrawDebugLaserLines()
        {
            foreach (RaycastHit scanHit in Scans)
            {
                Debug.DrawLine(tbc.BaseScanLink.position, scanHit.point, Color.HSVToRGB(scanHit.distance / MaxLaserRage, 1, 1));
            }
        }
    }
}
