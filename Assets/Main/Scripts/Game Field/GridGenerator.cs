using System;
using EasyButtons;
using UnityEngine;

namespace Main.Scripts.Game_Field
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private Grid _gridPrefab;
        [SerializeField] private int _columns;
        [SerializeField] private int _rows;
        [SerializeField] private Transform _bottomCenter;
        private Grid _currentGrid;

        [Button]
        public void Generate()
        {
            if(_currentGrid!=null)
                DestroyImmediate(_currentGrid.gameObject);
            _currentGrid = Instantiate(_gridPrefab, transform);
            var gridXScale = _gridPrefab.transform.lossyScale.x;
            var gridYScale = _gridPrefab.transform.lossyScale.y;
            _currentGrid.transform.name = "New Grid";
            var firstCellPoint  = new Vector2();
            firstCellPoint.x = CalcXPosForFirstCell(gridXScale);
            firstCellPoint.y = CalcYPosForFirstCell(gridYScale);
            var gridCells = new GridCell[_rows,_columns];

            for (int i = 0; i < _rows; i++)
            {
                var row = new GameObject();
                row.transform.SetParent(_currentGrid.transform);
                row.transform.position = new Vector3(0,firstCellPoint.y+gridYScale*i,0);
                row.transform.name = $"row_{i}";
                for (int j = 0; j < _columns; j++)
                {
                    var cell = Instantiate(_cellPrefab);
                    var offSet = new Vector2(gridXScale*j,gridYScale*i);
                    cell.transform.position = firstCellPoint + offSet;
                    cell.Setup(new GridCellCoordinates(){Row = i, Column = j});
                    cell.transform.name = $"cell_{j}";
                    cell.transform.SetParent(row.transform);
                    gridCells[i, j] = cell;
                }
            }
            _currentGrid.Setup(gridCells,new GridSize(){Rows = _rows, Columns = _columns});
        }

        private float CalcXPosForFirstCell(float cellXScale)
        {
            float xPos = 0;
            if (_columns % 2 == 0)
            {
                var halfGridOffset = cellXScale * ((float)_columns / 2);
                var centerOfCell = cellXScale / 2;
                xPos = transform.position.x - halfGridOffset + centerOfCell;
            }
            else
            {
                var halfGridOffset = cellXScale * ((float)(_columns-1) / 2);
                xPos = transform.position.x - halfGridOffset ;
            }

            return xPos;
        }
        private float CalcYPosForFirstCell(float cellYScale)
        {
            var yPos = _bottomCenter.position.y + cellYScale/2;
            return yPos;
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
