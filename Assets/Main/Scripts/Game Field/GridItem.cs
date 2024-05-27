using System;
using EasyButtons;
using Main.Scripts.Block;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main.Scripts.Game_Field
{
    public class GridItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private GridItemCoordinates _coordinates;
        private BaseBlock _currentBlock;
        public bool ContainBlock => _currentBlock != null;
        public BaseBlock CurrentBlock => _currentBlock;

        public GridItemCoordinates GetCoordinates() => _coordinates;

        public void Setup(GridItemCoordinates coordinates)
            => _coordinates = coordinates;

        [Button]
        public void SetBlock(BaseBlock block)=>
            _currentBlock = block;
        
        public BaseBlock GetBlock()
        {
            var result = _currentBlock;
            _currentBlock = null;
            return result;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position,new Vector3(1,1,0.1f));
        }
    }

    public struct GridItemCoordinates
    {
        public int Row;
        public int Column;
    }
}
