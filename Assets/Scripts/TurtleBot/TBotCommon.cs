using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer {
    public class TBotCommon : MonoBehaviour {
        public static string base_footprint = "base_footprint";
        public static string base_link = string.Join("/", base_footprint, "base_link");
        public static string wheel_left_link = string.Join("/", base_link, "wheel_left_link");
        public static string wheel_right_link = string.Join("/", base_link, "wheel_right_link");
        public static string base_scan = string.Join("/", base_link, "base_scan");
        public static string camera_link = string.Join("/", base_link, "camera_link");

        Transform baseLink, baseScanLink, cameraLink;
        Rigidbody baseLinkRigidBody;
        HingeJoint leftWheelHinge, rightWheelHinge;
        Camera cam;
        float wheelDist;

        private void Awake() {
            BaseLink.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0.00f, 0);
        }

        public HingeJoint LeftWheelHinge {
            get {
                if (!leftWheelHinge) {
                    leftWheelHinge = GetLink(wheel_left_link).GetComponent<HingeJoint>();
                }
                return leftWheelHinge;
            }
            set { leftWheelHinge = value; }
        }
        public HingeJoint RightWheelHinge {
            get {
                if (!rightWheelHinge) {
                    rightWheelHinge = GetLink(wheel_right_link).GetComponent<HingeJoint>();
                }
                return rightWheelHinge;
            }
            set { rightWheelHinge = value; }
        }

        public Transform BaseLink {
            get {
                if (!baseLink) {
                    baseLink = GetLink(base_link);
                }
                return baseLink;
            }
        }

        public Transform BaseScanLink {
            get {
                if (!baseScanLink) {
                    baseScanLink = GetLink(base_scan);
                }
                return baseScanLink;
            }
        }

        public Transform CameraLink {
            get {
                if (!cameraLink) {
                    cameraLink = GetLink(camera_link);
                }
                return cameraLink;
            }
        }

        public Camera Camera {
            get {
                if (!cam) {
                    cam = CameraLink.GetComponent<Camera>();
                }
                return cam;
            }
        }

        public Vector3 Position {
            get { return BaseLink.position; }
        }

        public Quaternion Rotation {
            get { return BaseLink.rotation; }
        }

        public Vector3 Heading {
            get { return BaseLink.forward; }
        }

        public Vector3 Velocity {
            get {
                if (!baseLinkRigidBody) {
                    baseLinkRigidBody = BaseLink.GetComponent<Rigidbody>();
                }
                return baseLinkRigidBody.velocity;
            }
        }

        public Transform GetLink(string s) {
            return transform.Find(s);
        }


        public float GetRotationY() {
            return BaseLink.rotation.eulerAngles.y;
        }

        public float WheelDist {
            get {
                if (wheelDist == 0) {
                    wheelDist = Vector3.Distance(LeftWheelHinge.transform.position, RightWheelHinge.transform.position);
                }
                return wheelDist;
            }
        }

    }
}
