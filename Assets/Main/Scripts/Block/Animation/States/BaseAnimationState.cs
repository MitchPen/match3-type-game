using Main.Scripts.StateMachine;
using UnityEngine;

namespace Main.Scripts.Block.Animation
{
    public abstract class BaseAnimationState : BaseState
    {
        protected AnimationStateData _data;
        protected Sprite[] _frames;
        protected int _frameIndex;

        protected BaseAnimationState(AnimationStateData data, Sprite[] frames)
        {
            _data = data;
            _frames = frames;
            _frameIndex = 0;
        }
    }

    public class AnimationStateData
    {
        public SpriteRenderer Renderer;
        public int FrameDelay;
    }
}
