using System;
using System.Collections.Generic;
using Main.Scripts.Game_Field.States;
using Main.Scripts.StateMachine;
using UnityEngine;

namespace Main.Scripts.Block.Animation
{
    public class BlockAnimator : MonoBehaviour
    {
        private DefaultStateMachine<BaseAnimationState> _stateMachine;

        public void Setup(SpriteRenderer renderer, AnimationSetup Setup)
        {
            var stateData = new AnimationStateData()
            {
                Renderer = renderer,
                FrameDelay = Setup.GetFrameDelay(),
            };
            var animationStates = new Dictionary<Type, BaseAnimationState>()
            {
                {
                    typeof(IdleAnimationState), new IdleAnimationState(stateData,
                        Setup.GetFrameSequence(AnimationType.IDLE))
                }
            };
            _stateMachine = new DefaultStateMachine<BaseAnimationState>();
            _stateMachine.InitializeStates(animationStates);
        }

        public void PlayAnimation(AnimationType Type,Action onEnter = null, Action onExit = null)
        {
            switch (Type)
            {
                case AnimationType.IDLE:_stateMachine.SwitchState<IdleAnimationState>(onEnter, onExit); break;
                case AnimationType.DESTROY:_stateMachine.SwitchState<IdleAnimationState>(onEnter, onExit); break;
            }
        }
        
    }
}