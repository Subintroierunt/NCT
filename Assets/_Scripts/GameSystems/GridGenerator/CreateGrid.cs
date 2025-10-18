using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems
{
    public class CreateGrid : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private Cell cell;
        private GridData gridData;
        private Vector2Int gridSize;

        private void Start()
        {
            gridData = new GridData(1);
            Init();
        }

        private void Init()
        {
            gridSize = new Vector2Int(gridData.Cells.GetLength(0), gridData.Cells.GetLength(1));
            gridLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * gridSize.x, 100 * gridSize.y);
            gridLayout.constraintCount = gridData.Cells.GetLength(0);
        }

        private void CreateCell()
        {

        }
    }
}
