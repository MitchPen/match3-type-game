using System;
using EasyButtons;
using UnityEngine;

namespace Main.Scripts.Game_Field
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GridItem _prefab;
        [SerializeField] private int _colls;
        [SerializeField] private int _rows;
        [SerializeField] private Transform _bottomCenter;
        private Grid _currentGrid;

        [Button]
        public void Generate()
        {
            
        }

        private void OnDrawGizmos()
        {
            if (_bottomCenter != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(_bottomCenter.position, 0.2f);
            }
        }
    }
}
