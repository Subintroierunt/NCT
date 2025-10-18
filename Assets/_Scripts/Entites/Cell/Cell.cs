using UnityEngine;

namespace Entities
{
    public class Cell : MonoBehaviour
    {
        private Vector2Int coords;
        
        public Vector2Int Coords => coords;

        public void Init(Vector2Int coords)
        {
            this.coords = coords;
        }
    }
}
