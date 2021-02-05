using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotLaserScanner : MonoBehaviour
    {
        [SerializeField]
        int numLaserScanLines, maxLaserRange;

        TBotCommon tbc;

        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }

        private void FixedUpdate()
        {
            UpdateLaserScan();
        }

        private void UpdateLaserScan()
        {
            float angle = 0;
            for (int i = 0; i < numLaserScanLines; i++)
            {
                Vector3 dir = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
                RaycastHit hit;

                if (Physics.Raycast(tbc.BaseScanLink.position, dir, out hit))
                {
                    Debug.DrawLine(tbc.BaseScanLink.position, hit.point, Color.HSVToRGB(Vector3.Distance(hit.point, tbc.BaseScanLink.position) / maxLaserRange, 1, 1));
                }
                else
                {
                    Debug.DrawLine(tbc.BaseScanLink.position, tbc.BaseScanLink.position + dir * maxLaserRange, Color.red);
                }
                angle += 2 * Mathf.PI / numLaserScanLines;
            }
        }
    }
}
