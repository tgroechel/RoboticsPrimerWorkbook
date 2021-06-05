using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoboticsPrimer
{
    public class KTextUI : MonoBehaviour
    {
        Text text;
        string baseString = "K: ";

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        internal void UpdateText(int numClusterK)
        {
            text.text = string.Join("", baseString, numClusterK.ToString());
        }
    }
}
