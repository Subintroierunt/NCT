using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
    public struct GridData
    {
        public CellData[,] Cells;

        public GridData(int arg1)
        {
            Cells = new CellData[5,5];
        }
    }
}
