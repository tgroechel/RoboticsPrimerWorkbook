using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer {
    public class PlayerUIManager : Singleton<PlayerUIManager> {

        RawImage rawImage;
        public RawImage TBotCameraUIImage {
            get {
                if (rawImage == null) {
                    rawImage = GetComponentInChildren<RawImage>();
                }
                return rawImage;
            }
            set {
                rawImage = value;
            }
        }

        public void TurnCameraUIOn() {
            SetCameraUIState(true);
        }

        public void TurnCameraUIOff() {
            SetCameraUIState(false);
        }

        public void SetCameraUIState(bool state) {
            TBotCameraUIImage.enabled = state;
        }

    }
}
