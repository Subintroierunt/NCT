using UnityEngine;

namespace StaticData
{
    public struct GridData
    {
        public Vector2Int[,] FromToCupPoints;

        public CellData[,] Cells;

        public GridData(int arg1)
        {
            FromToCupPoints = new Vector2Int[2, 2];
            FromToCupPoints[0, 0] = new Vector2Int(0, 0);
            FromToCupPoints[0, 1] = new Vector2Int(4, 0);
            FromToCupPoints[1, 0] = new Vector2Int(0, 4);
            FromToCupPoints[1, 1] = new Vector2Int(4, 4);

            Cells = new CellData[5,5];
            Cells[0, 0] = new CellData(Entities.CellType.Node);
            Cells[2, 0] = new CellData(Entities.CellType.Node);
            Cells[4, 0] = new CellData(Entities.CellType.Node);
            Cells[2, 2] = new CellData(Entities.CellType.Node);
            Cells[0, 4] = new CellData(Entities.CellType.Node);
            Cells[2, 4] = new CellData(Entities.CellType.Node);
            Cells[4, 4] = new CellData(Entities.CellType.Node);

            Cells[1, 0] = new CellData(Entities.CellType.Connection);
            Cells[3, 0] = new CellData(Entities.CellType.Connection);
            Cells[2, 1] = new CellData(Entities.CellType.Connection);
            Cells[2, 3] = new CellData(Entities.CellType.Connection);
            Cells[1, 4] = new CellData(Entities.CellType.Connection);
            Cells[3, 4] = new CellData(Entities.CellType.Connection);
        }
    }
}
