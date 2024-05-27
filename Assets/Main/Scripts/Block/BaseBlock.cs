using Cysharp.Threading.Tasks;
using Main.Scripts.Block.Animation;
using UnityEngine;

namespace Main.Scripts.Block
{
    public class BaseBlock : MonoBehaviour
    {
        [SerializeField] private BlockType _type;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BlockAnimator _animator;
        [SerializeField] private AnimationSetup _animationSetup;
        private int _currentRow = 0;
        protected bool _lock;
        
        public BlockType Type => _type;
        public bool AbleToMove => _lock;
        public int CurrentRow => _currentRow;

        public void Start()
        {
            Init(2);
        }

        public void Init(int currentRow)
        {
            _animator.Setup(_spriteRenderer, _animationSetup);
            UpdateSortingLayer(currentRow);
            _animator.PlayAnimation(AnimationType.IDLE);
            _spriteRenderer.gameObject.SetActive(true);
            _lock = false;
        }

        public void UpdateSortingLayer(int row)
        {
            _currentRow = row;
            _spriteRenderer.sortingOrder = _currentRow;
        }
        
        public void Move(Transform to)
        {
            _lock = true;
            //for test
            transform.position = to.position;
            //todo 
            // change to DOTween motion 
            _lock = false;
        }

        public async UniTask Destroy()
        {
            _lock = true;
            _animator.PlayAnimation(AnimationType.DESTROY);
        
        }
    }
}
