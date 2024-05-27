using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Main.Scripts.Block.Animation
{
    public class IdleAnimationState : BaseAnimationState
    {
        public IdleAnimationState(AnimationStateData data, Sprite[] frames) : base(data,frames) { }
        private bool _loop = false;
       
        public override void Enter(Action onEnter=null, Action onExit=null)
        {
            base.Enter(onEnter, onExit);
            ResetToken();
            _ctx = new CancellationTokenSource();
            _frameIndex = GenerateRandomFrameIndex();
            _loop = true;
            PlayIdleAnimation();
        }

        private int GenerateRandomFrameIndex()
        {
            int index = 0;
            index = UnityEngine.Random.Range(0, _frames.Length-1);
            return index;
        }

        private async UniTaskVoid PlayIdleAnimation()
        {
            while (_loop)
            {
                _data.Renderer.sprite = _frames[_frameIndex];
                _frameIndex++;
                if (_frameIndex >= _frames.Length)
                    _frameIndex = 0;
                await UniTask.Delay(_data.FrameDelay, cancellationToken: _ctx.Token)
                    .SuppressCancellationThrow();
            }
        }

        public override void Exit()
        {
            _loop = false;
            ResetToken();
            base.Exit();
        }
    }
}
