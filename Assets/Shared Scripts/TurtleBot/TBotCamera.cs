using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotCamera : MonoBehaviour
    {
        TBotCommon tbc;
        private void Awake()
        {
            tbc = GetComponent<TBotCommon>();
        }
        private void FixedUpdate()
        {
            PlayerUIManager.instance.TBotCameraUIImage.texture = tbc.Camera.activeTexture;
        }
    }
}