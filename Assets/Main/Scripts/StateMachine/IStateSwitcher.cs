using System;

namespace Main.Scripts.StateMachine
{
    public interface IStateSwitcher<T>  where T: IState
    {
        public void SwitchState<T>(Action OnEntire = null, Action OnExit = null);
    }
}
