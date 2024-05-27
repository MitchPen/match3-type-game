using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Block.Animation
{
    [CreateAssetMenu(fileName = "AnimationSetup", menuName = "Configs/Block/AnimationSetup", order = 1)]
    public class AnimationSetup : ScriptableObject
    {
        [SerializeField] private int _fps;
        [SerializeField] private List<AnimationFrameSequence> _frameSequences = new List<AnimationFrameSequence>();
        public int GetFrameDelay() => Mathf.CeilToInt((float) TimeSpan.FromSeconds(1).TotalMilliseconds / _fps);

        public Sprite[] GetFrameSequence(AnimationType type)
        {
            var sequence = _frameSequences
                .Find(s => s.Type == type).Sprites;
            return sequence;
        }
    }

    [Serializable]
    public class AnimationFrameSequence
    {
        public AnimationType Type;
        public Sprite[] Sprites;
    }

    public enum AnimationType
    {
        DEFAULT,
        IDLE,
        DESTROY
    }
}
