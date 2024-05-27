using System;
using System.Threading;

namespace Main.Scripts.StateMachine
{
    public abstract class BaseState : IState
    {
        protected Action _onEnter;
        protected Action _onExit;
        protected CancellationTokenSource _ctx;

        public virtual void Enter(Action onEnter = null, Action onExit = null)
        {
            _onEnter = onEnter;
            _onExit = onExit;
            _onEnter?.Invoke();
        }

        public virtual void Exit()
        {
            _onExit?.Invoke();
        }
    
        protected void ResetToken()
        {
            if (_ctx == null) return;
            if (!_ctx.IsCancellationRequested)
                _ctx.Cancel();
            _ctx.Dispose();
        }
    }
}
