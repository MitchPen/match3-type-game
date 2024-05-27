using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main.Scripts.Game_Field.States
{
    public class DestroyPairsState : BaseGridState
    {
        public DestroyPairsState(GridStateData data) : base(data) { }

        public async override void Enter(Action onEnter = null, Action onExit = null)
        {
            base.Enter(onEnter, onExit);
            ResetToken();
            _ctx = new CancellationTokenSource();
            await DestroyPair();
            if(CheckForFinishGame())
                Debug.Log("finish");
            else
                _data.StateSwitcher.SwitchState<GridNormalizationState>();
        }

        public override void Exit()
        {
            ResetToken();
            base.Exit();
        }

        private async UniTask DestroyPair()
        {
            if (_data.BlocksToDestroy.Count > 0)
            {
                var tasks = new List<UniTask>();
                foreach (var block in _data.BlocksToDestroy)
                {
                    var task = block.Destroy();
                    tasks.Add(task);
                }

                await UniTask.WhenAll(tasks.ToArray())
                    .SuppressCancellationThrow();
                _data.BlocksToDestroy.Clear();
            }
        }

        private bool CheckForFinishGame()
        {
            for (int i = 0; i < _data.Size.Rows - 1; i++)
            {
                for (int j = 0; j < _data.Size.Columns - 1; j++)
                {
                    if (_data.GridItems[i, j].ContainBlock)
                        return false;
                }
            }

            return true;
        }
    }
}
