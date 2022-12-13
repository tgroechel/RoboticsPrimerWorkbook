using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotLaserScanner : MonoBehaviour
    {
#region MEMBERS
        [field: SerializeField]
        public int NumLaserScans
        {
            get; set;
        }
        [field: SerializeField]
        public float MaxLaserRange
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
#endregion
#region ENGINE
        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void FixedUpdate()
        {
            UpdateLaserScan();
            DrawDebugLaserLines();
        }
#endregion
#region CODE

        private void UpdateLaserScan()
        {
           // /* SOLUTION
            float angle = 0;
            for (int i = 0; i < NumLaserScans; i++)
            {
                Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                RaycastHit hit;

                if (!Physics.Raycast(tbc.BaseScanLink.position, dir, out hit, MaxLaserRange))
                {
                    hit.point = tbc.BaseScanLink.position + dir * MaxLaserRange;
                    hit.distance = MaxLaserRange;
                }

                Scans[i] = hit;
                angle += 2 * Mathf.PI / NumLaserScans;
            }
          //  END */
        }
#endregion
#region HELPERS AND UI
        private void DrawDebugLaserLines()
        {
            foreach (RaycastHit scanHit in Scans)
            {
                Debug.DrawLine(tbc.BaseScanLink.position, scanHit.point, Color.HSVToRGB(scanHit.distance / MaxLaserRange, 1, 1));
            }
        }
#endregion
    }
}
