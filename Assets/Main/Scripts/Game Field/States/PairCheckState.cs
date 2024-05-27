using System;
using System.Collections.Generic;
using Main.Scripts.Block;

namespace Main.Scripts.Game_Field.States
{
    public class PairCheckState : BaseGridState
    {
        public PairCheckState(GridStateData data) : base(data) { }

        public override void Enter(Action onEnter = null, Action onExit = null)
        {
            base.Enter(onEnter, onExit);
            CheckForPairSequence();
            if (_data.BlocksToDestroy.Count != 0)
                _data.StateSwitcher.SwitchState<DestroyPairsState>();
            else
                _data.StateSwitcher.SwitchState<GridAwaitingState>();
        }

        public override void Exit()
        {
            base.Exit();
        }


        public void CheckForPairSequence()
        {
            for (int i = 0; i < _data.Size.Rows; i++)
            {
                for (int j = 0; j < _data.Size.Columns; j++)
                {
                    var item = _data.GridItems[i, j];
                    if (!item.ContainBlock) continue;
                    var nearbyBlocks = CheckNearbyBlocks(item);
                    if (nearbyBlocks.Length <= 1 || PairOnSameRowCount(nearbyBlocks, i) <= 1) continue;

                    AddBlockToDestroyOrder(item.CurrentBlock);
                    foreach (var block in nearbyBlocks)
                        AddBlockToDestroyOrder(block);
                }
            }
        }

        private BaseBlock[] CheckNearbyBlocks(GridCell cell)
        {
            var pos = cell.GetCoordinates();
            var nearbyPositions = new (int row, int col)[]
            {
                (pos.Row, pos.Column - 1),
                (pos.Row - 1, pos.Column),
                (pos.Row, pos.Column + 1),
                (pos.Row + 1, pos.Column),
            };
            var pairs = new List<BaseBlock>();
            foreach (var nearbyPosition in nearbyPositions)
            {
                var result = CheckBlockOnPosition(nearbyPosition.row, nearbyPosition.col);
                if (result != null && result.Type == cell.CurrentBlock.Type)
                    pairs.Add(result);
            }

            return pairs.ToArray();
        }

        private BaseBlock CheckBlockOnPosition(int row, int coll)
        {
            if (row < 0 && row >= _data.Size.Rows) return null;
            if (coll < 0 && coll >= _data.Size.Columns) return null;
            return _data.GridItems[row, coll].ContainBlock ? _data.GridItems[row, coll].CurrentBlock : null;
        }

        private int PairOnSameRowCount(IEnumerable<BaseBlock> nearbyBlocks, int row)
        {
            int sameRowCount = 0;
            foreach (var nearbyBlock in nearbyBlocks)
            {
                if (nearbyBlock.CurrentRow == row)
                    sameRowCount++;
            }

            return sameRowCount;
        }

        private void AddBlockToDestroyOrder(BaseBlock block)
        {
            if (!_data.BlocksToDestroy.Contains(block))
                _data.BlocksToDestroy.Add(block);
        }

        //___________OLD_SOLUTION_____________________________

        // private bool CheckForPairsSequence()
        // {
        //     CheckHorizontalPair();
        //     CheckVerticalPair();
        //     return _blocksToDestroy.Count > 0;
        // }
        //
        // private void CheckHorizontalPair()
        // {
        //     BaseBlock prevBlock = null;
        //     for (int i = 0; i < _size.Rows - 1; i++)
        //     {
        //         for (int j = 0; j < _size.Colls - 1; j++)
        //         {
        //             if (_gridItems[i, j].ContainBlock)
        //             {
        //                 CheckPair(prevBlock, _gridItems[i, j].CurrentBlock);
        //                 prevBlock = _gridItems[i, j].CurrentBlock;
        //             }
        //             else
        //                 prevBlock = null;
        //         }
        //     }
        // }
        //
        // private void CheckVerticalPair()
        // {
        //     BaseBlock prevBlock = null;
        //     for (int j = 0; j <= _size.Colls - 1; j++)
        //     {
        //         for (int i = 0; i <=_size.Rows -1; i++)
        //         {
        //             if (!_gridItems[j, i].ContainBlock) break;
        //             
        //             CheckPair(prevBlock, _gridItems[i, j].CurrentBlock);
        //             prevBlock = _gridItems[i, j].CurrentBlock;
        //         }
        //     }
        // }
        //
        // private void CheckPair(BaseBlock prevBlock, BaseBlock current)
        // {
        //     if (current.Type != prevBlock.Type) return;
        //     AddBlockToPairTable(prevBlock);
        //     AddBlockToPairTable(current);
        // }
    }
}