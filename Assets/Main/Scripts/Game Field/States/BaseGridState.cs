using System;
using System.Collections.Generic;
using Main.Scripts.Block;
using Main.Scripts.StateMachine;

namespace Main.Scripts.Game_Field.States
{
    public class BaseGridState : BaseState
    {
        protected GridStateData _data;
        protected BaseGridState(GridStateData data) =>_data = data;
    }
    
    public class GridStateData
    {
        public IStateSwitcher<BaseGridState> StateSwitcher;
        public GridItem[,] GridItems;
        public GridSize Size;
        public HashSet<BaseBlock> BlocksToDestroy = new HashSet<BaseBlock>();
        public float BlockMoveTime;
        public Action<GridItem, GridItem, float> MoveBlock;
    }
}
