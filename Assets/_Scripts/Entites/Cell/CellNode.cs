using StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class CellNode : Cell
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CupConfig config;
        
        private int target;
        private bool isOccupied;
        private bool isMatch;
        private Vector2Int fromPath;

        private List<Vector2Int> adjacentNodes = new List<Vector2Int>();

        public event Action CupEntered;
        public List<Vector2Int> AdjacentNodes => adjacentNodes;
        public Vector2Int FromPath => fromPath;
        public bool IsOccupied => isOccupied;
        public bool IsMatch => isMatch;

        public void CupEnter(int enteredNumber)
        {
            isMatch = target == enteredNumber;
            isOccupied = true;
            CupEntered?.Invoke();
        }
            
        public void CupExit()
        {
            isMatch = false;
            isOccupied = false;
        }
            
        public void SetFromPath(Vector2Int fromPath) => 
            this.fromPath = fromPath;

        public void AddAdjacent(Vector2Int coords) => 
            adjacentNodes.Add(coords);

        public void SetRequirement(int target)
        {
            this.target = target;
            transform.localScale = Vector3.one;
            spriteRenderer.color = config.Colors[target];
        }
    }
}

