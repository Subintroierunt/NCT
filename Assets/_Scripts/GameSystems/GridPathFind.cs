using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Entities;

namespace GameSystems
{
    public class GridPathFind : MonoBehaviour
    {
        [SerializeField] private PathGhost pathGhostPrefab;
        [SerializeField] private Transform ghostRoot;
        [SerializeField] private CupMovement cupMovement;

        public event Action<Vector2Int> PathSelected;

        private List<Vector2Int> pathPoints;
        private Dictionary<Vector2Int, Cell> cellList;
        private List<PathGhost> pathGhosts;
        private Queue<PathGhost> pathGhostPool;

        public void Init(Dictionary<Vector2Int, Cell> cellList)
        {
            this.cellList = cellList;
            pathGhostPool = new Queue<PathGhost>();
            pathGhosts = new List<PathGhost>();
        }

        public void CalculatePath(Vector2Int start)
        {
            HidePathGhost();
            pathPoints = new List<Vector2Int>();
            pathPoints.Add(start);

            Queue<CellNode> checkNodes = new Queue<CellNode>();
            List<Vector2Int> nodeCoords = ((CellNode)cellList[start]).AdjacentNodes;
            CellNode source = ((CellNode)cellList[start]);

            int fuse = 0;
            while (fuse < 1000)
            {
                fuse++;

                for (int i = 0; i < nodeCoords.Count; i++)
                {
                    if (!pathPoints.Contains(nodeCoords[i]))
                    {
                        pathPoints.Add(nodeCoords[i]);
                        ((CellNode)cellList[nodeCoords[i]]).SetFromPath(source.Coords);
                        if (!((CellNode)cellList[nodeCoords[i]]).IsOccupied)
                        {
                            checkNodes.Enqueue((CellNode)cellList[nodeCoords[i]]);
                        }
                    }
                }
                if (checkNodes.Count > 0)
                {
                    source = checkNodes.Dequeue();
                    nodeCoords = source.AdjacentNodes;
                }
                else
                {
                    break;
                }
            }

            foreach (Vector2Int node in pathPoints)
            {
                if (!((CellNode)cellList[node]).IsOccupied)
                {
                    ShowPathGhost(node);
                }
            }
        }

        private void ShowPathGhost(Vector2Int coord)
        {
            GetGhost().Init(coord);
        }

        private void HidePathGhost()
        {
            foreach (PathGhost node in pathGhosts)
            {
                node.gameObject.SetActive(false);
                pathGhostPool.Enqueue(node);
            }
            pathGhosts.Clear();
        }

        private PathGhost GetGhost()
        {
            if (pathGhostPool.Count > 0)
            {
                pathGhosts.Add(pathGhostPool.Dequeue());
            }
            else
            {
                pathGhosts.Add(Instantiate(pathGhostPrefab, ghostRoot));
                cupMovement.AddPathGhost(pathGhosts.Last());
                pathGhosts.Last().PathSelected += _ => HidePathGhost();
            }
            pathGhosts.Last().gameObject.SetActive(true);
            return pathGhosts.Last();
        }
    }
}
