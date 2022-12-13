using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsPrimer
{
    public abstract class TBotPlanner : MonoBehaviour
    {
        protected TBotCommon tbc;
        protected TBotTurnGoTurn botTurnGoTurn;
        void Start()
        {
            tbc = GetComponent<TBotCommon>();
            botTurnGoTurn = GetComponent<TBotTurnGoTurn>();
            botTurnGoTurn.enabled = true;
            MazeManager.instance.MazeHasBeenGenerated.AddListener(CreateAndVisualizePlan);
        }
        public abstract void SendNextGoal();
        public abstract void CreateAndVisualizePlan();
    }
}
