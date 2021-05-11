using UnityEngine;


namespace RoboticsPrimer {
    public class TBotNavigationState : MonoBehaviour {
        public enum ROBOT_NAV_STATE {
            NAVIGATING,
            WAITINGFORNAVGOAL,
            RECEIVEDNAVGOAL,
            ATGOAL,
            STUCK
        }

        ROBOT_NAV_STATE curState = ROBOT_NAV_STATE.WAITINGFORNAVGOAL;
        public ROBOT_NAV_STATE CurState {
            get {
                return curState;
            }
            set {
                curState = value;
            }
        }
    }
}
