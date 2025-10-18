using System;
using UnityEngine;

namespace Entities
{
    public class PathGhost : MonoBehaviour
    {
        public event Action<Vector2Int> PathSelected;

        private Vector2Int coords;

        public void Init(Vector2Int coords)
        {
            this.coords = coords;
            transform.position = new Vector3(coords.x, coords.y, -2);
        }

        private void OnMouseDown()
        {
            PathSelected?.Invoke(coords);
        }
    }
}
