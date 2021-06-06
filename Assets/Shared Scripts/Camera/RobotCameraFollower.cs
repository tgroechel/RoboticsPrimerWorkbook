
using UnityEngine;

namespace RoboticsPrimer
{
    public class RobotCameraFollower : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.3F;
        private Vector3 velocity = Vector3.zero;
        private Vector3 overviewVec;

        private void Start()
        {
            if (target == null)
            {
                target = FindObjectOfType<TBotCommon>().GetLink(TBotCommon.base_link).transform;
                overviewVec = transform.position;
            }
        }

        void Update()
        {
            Vector3 targetPosition = target.TransformPoint(overviewVec);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
