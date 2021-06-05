using UnityEngine;

namespace RoboticsPrimer
{
    public class TBotHighLevelNavPlanner : MonoBehaviour
    {
        [SerializeField]
        bool manualPlan;

        TBotPlanner tBotPlanner;

        public static readonly int[] S = { 0, 0 };  // Stay
        public static readonly int[] U = { -1, 0 }; // Up
        public static readonly int[] D = { 1, 0 };  // Down
        public static readonly int[] L = { 0, -1 }; // Left
        public static readonly int[] R = { 0, 1 };  // Right

        private void Awake()
        {
            if (!manualPlan)
            {
                tBotPlanner = gameObject.AddComponent<TBotAutonomousPlanner>();

            }
            else
            {
                tBotPlanner = gameObject.AddComponent<TBotManualPlanner>();
            }
        }

        public void AskForNextGoal()
        {
            tBotPlanner.SendNextGoal();
        }
    }
}