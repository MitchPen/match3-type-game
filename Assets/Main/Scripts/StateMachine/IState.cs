using System;

namespace Main.Scripts.StateMachine
{
    public interface IState
    {
        public void Enter(Action onEnter=null, Action onExit=null);
        public void Exit();
    }
}
