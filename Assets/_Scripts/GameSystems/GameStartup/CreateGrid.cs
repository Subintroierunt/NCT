using Entities;
using StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystems
{
    public class CreateGrid : MonoBehaviour
    {
        [SerializeField] private Transform nodeRoot;
        [SerializeField] private Transform connectionRoot;
        [SerializeField] private CellNode nodePrefab;
        [SerializeField] private CellConnection connectionPrefab;
        [SerializeField] private WinCondition winCondition;

        private GridData gridData;
        private Vector2Int gridSize;
        private Dictionary<Vector2Int, Cell> cellList;

        public Dictionary<Vector2Int, Cell> CellList =>
            cellList;

        public void Init(GridData gridData)
        {
            this.gridData = gridData;

            cellList = new Dictionary<Vector2Int, Cell> ();
            gridSize = new Vector2Int(gridData.Cells.GetLength(0), gridData.Cells.GetLength(1));
            Camera.main.transform.position = new Vector3((float)gridSize.x / 2, (float)gridSize.y / 2, -10);

            for (int i = 0; i < gridSize.y; i++)
            {
                for (int j = 0; j < gridSize.x; j++)
                {
                    if (gridData.Cells[j,i].CellType != CellType.None)
                    {
                        CreateCell(gridData.Cells[j, i].CellType, new Vector2Int(j,i));
                    }
                }
            }

            for (int i = 0; i < cellList.Count; i++)
            {
                if (cellList.ElementAt(i).Value.GetType() == typeof(CellConnection))
                {
                    CreateConnection(cellList.ElementAt(i).Value as CellConnection);
                }
            }

            for (int i = 0; i < gridData.FromToCupPoints.GetLength(0); i++)
            {
                InitNode(cellList[gridData.FromToCupPoints[i, 1]] as CellNode, i);
            }
        }

        private void CreateCell(CellType cellType, Vector2Int coords)
        {
            Cell obj;

            switch (cellType)
            {
                case CellType.Node:
                    obj = Instantiate(nodePrefab, nodeRoot);
                    break;
                case CellType.Connection:
                    obj = Instantiate(connectionPrefab, connectionRoot);
                    break;
                default:
                    return;
            }

            obj.Init(coords);
            obj.transform.position = (Vector3)(Vector2)(coords);

            cellList.Add(coords, obj);
        }

        private void CreateConnection(CellConnection connection)
        {
            if (connection.Coords.x % 2 == 1)
            {
                connection.SetConnectedNodes(
                    (CellNode)cellList[new Vector2Int(connection.Coords.x - 1, connection.Coords.y)],
                    (CellNode)cellList[new Vector2Int(connection.Coords.x + 1, connection.Coords.y)]);
            }
            else
            {
                if (connection.Coords.y % 2 == 1)
                {
                    connection.SetConnectedNodes(
                        (CellNode)cellList[new Vector2Int(connection.Coords.x, connection.Coords.y - 1)],
                        (CellNode)cellList[new Vector2Int(connection.Coords.x, connection.Coords.y + 1)]);
                }
            }
        }

        private void InitNode(CellNode node, int target)
        {
            node.SetRequirement(target);
            winCondition.AddNodes(node);
        }
    }
}
