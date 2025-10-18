using Entities;

namespace StaticData
{
    public struct CellData
    {
        public CellType CellType;

        public CellData(CellType cellType)
        {
            CellType = cellType;
        }
    }
}
