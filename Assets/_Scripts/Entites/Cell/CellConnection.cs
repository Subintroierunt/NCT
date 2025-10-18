using UnityEngine;

namespace Entities
{
    public class CellConnection : Cell
    {
        [SerializeField] private LineRenderer lineRenderer;

        public void SetConnectedNodes(CellNode from, CellNode to)
        {
            from.AddAdjacent(to.Coords);
            to.AddAdjacent(from.Coords);
            lineRenderer.SetPosition(0, new Vector3(from.Coords.x, from.Coords.y, 1));
            lineRenderer.SetPosition(1, new Vector3(to.Coords.x, to.Coords.y, 1));
        }
    }
}
