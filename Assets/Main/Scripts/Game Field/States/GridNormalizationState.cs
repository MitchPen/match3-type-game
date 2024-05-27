using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main.Scripts.Game_Field.States
{
    public class GridNormalizationState : BaseGridState
    {
        public GridNormalizationState(GridStateData data) : base(data) { }
        protected int _moveTimingPerCeil;

        public override async void Enter(Action onEnter = null, Action onExit = null)
        {
            base.Enter(onEnter, onExit);
            _moveTimingPerCeil =Mathf.CeilToInt(_data.BlockMoveTime * 1000);
            ResetToken();
            _ctx = new CancellationTokenSource();
            await Normalize();
            _data.StateSwitcher.SwitchState<PairCheckState>();
        }

        public override void Exit()
        {
            ResetToken();
            base.Exit();
        }

        private async UniTask Normalize()
        {
            int maxFallIndex = 0;
            for (int i = 0; i < _data.Size.Rows - 1; i++)
            {
                for (int j = 0; j < _data.Size.Columns - 1; j++)
                {
                    if (_data.GridItems[i, j].ContainBlock)
                    {
                        int lowerIndex = j - 1;
                        
                        while (lowerIndex >= 0)
                        {
                            if (_data.GridItems[i, lowerIndex].ContainBlock)
                            {
                                lowerIndex++;
                                break;
                            }

                            lowerIndex--;
                        }

                        if (lowerIndex == j) continue;
                        
                        var heightDifference = j - lowerIndex;
                        _data.MoveBlock(_data.GridItems[i, j], _data.GridItems[i, lowerIndex], heightDifference*_data.BlockMoveTime);

                        if (heightDifference > maxFallIndex)
                            maxFallIndex = heightDifference;
                    }
                }
            }

            if (maxFallIndex > 0)
                await UniTask.Delay(maxFallIndex* _moveTimingPerCeil,cancellationToken: _ctx.Token)
                    .SuppressCancellationThrow();
        }
    }
}
