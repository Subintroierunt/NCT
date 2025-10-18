using Entities;
using System.Collections.Generic;
using UnityEngine;
using StaticData;

namespace GameSystems
{
    public class CreateCup : MonoBehaviour
    {
        [SerializeField] private Cup cupPrefab;
        [SerializeField] private Transform cupRoot;
        [SerializeField] private GridPathFind pathFind;
        [SerializeField] private CupMovement cupMovement;

        private GridData gridData;

        public void Init(Dictionary<Vector2Int, Cell> cellList, GridData gridData)
        {
            this.gridData = gridData;

            for (int i = 0; i < gridData.FromToCupPoints.GetLength(0); i++)
            {
                Cup cup = Instantiate(cupPrefab, cupRoot);
                cup.transform.position = (Vector3)(Vector2)gridData.FromToCupPoints[i, 0] + Vector3.back;
                cup.Init(i, gridData.FromToCupPoints[i, 0], pathFind, cellList);
                cupMovement.AddCup(cup);
            }
        }
    }
}
