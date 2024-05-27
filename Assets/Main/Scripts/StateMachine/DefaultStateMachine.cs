using System;
using System.Collections.Generic;

namespace Main.Scripts.StateMachine
{
    public class DefaultStateMachine<T> : IStateSwitcher<T> where T : IState
    {
        private Dictionary<Type, T> _states;
        private T _currentState;

        public void InitializeStates(Dictionary<Type, T> states) =>
            _states = states;

        public void SwitchState<T>(Action onEnter = null, Action onExit = null)
        {
            if (!_states.ContainsKey(typeof(T))) return;

            var newState = _states[typeof(T)];
            if (_currentState != null)
            {
                if (_currentState.GetType() == newState.GetType()) return;
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter(onEnter, onExit);
        }
    }
}