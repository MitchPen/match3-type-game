using System;
using System.Collections.Generic;
using Main.Scripts.Block;
using Main.Scripts.Game_Field.States;
using Main.Scripts.StateMachine;
using UnityEngine;

namespace Main.Scripts.Game_Field
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GridCell[,] _gridItems;
        [SerializeField] private GridSize _size;
        private HashSet<BaseBlock> _blocksToDestroy = new HashSet<BaseBlock>();
        private DefaultStateMachine<BaseGridState> _stateMachine;
        public float blockMoveTime;

        public void Setup(GridCell[,] items, GridSize size)
        {
            _size = size;
            _gridItems = items;
        }

        public void Initialize()
        {
            var _stateMachine = new DefaultStateMachine<BaseGridState>();
            var _date = new GridStateData()
            {
                StateSwitcher = _stateMachine,
                GridItems = _gridItems,
                Size = _size,
                BlocksToDestroy = _blocksToDestroy,
                BlockMoveTime = blockMoveTime,
                MoveBlock  = MoveBlock
            };
            
            var gridStates = new Dictionary<Type, BaseGridState>()
            {
                {typeof(LoadingGridState),new LoadingGridState(_date)},
                {typeof(GridAwaitingState),new GridAwaitingState(_date)},
                {typeof(GridNormalizationState),new GridNormalizationState(_date)},
                {typeof(PairCheckState),new PairCheckState(_date)},
                {typeof(DestroyPairsState),new DestroyPairsState(_date)}
            };
            _stateMachine.InitializeStates(gridStates);
            _stateMachine.SwitchState<GridNormalizationState>();
        }
        
        private void MoveBlock(GridCell from, GridCell to, float time)
        {
            var mainBlock = from.GetBlock();
            
            if (to.ContainBlock)
            {
                var secondaryBlock = to.GetBlock();
                secondaryBlock.Move(from.transform);
                secondaryBlock.UpdateSortingLayer(from.GetCoordinates().Row);
                from.SetBlock(secondaryBlock);
            }

            mainBlock.Move(to.transform);
            mainBlock.UpdateSortingLayer(to.GetCoordinates().Row);
            to.SetBlock(mainBlock);
        }
        
        //todo
        //add swipe handling 
    }

    public struct GridSize
    {
        public int Columns;
        public int Rows;
    }
}