using GameSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StaticData;
using DG.Tweening;

namespace Entities
{
    public class Cup : MonoBehaviour
    {
        [SerializeField] private CupConfig config;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform outLine;

        public event Action<Cup> CupSelected;

        private Dictionary<Vector2Int, Cell> cellList;
        private Vector2Int coords;
        private int number;

        private GridPathFind pathFind;
        private bool isMoving;

        public Vector2Int Coords =>
            coords;

        private void OnMouseDown()
        {
            if (!isMoving)
            {
                pathFind.CalculatePath(coords);
                CupSelected?.Invoke(this);
            }
        }

        public void Select()
        {
            outLine.localScale = Vector3.one * 1.2f;
        }

        public void Deselect()
        {
            outLine.localScale = Vector3.one * 1.1f;
        }

        public void Init(int number, Vector2Int coords, GridPathFind pathFind, Dictionary<Vector2Int, Cell> cellList)
        {
            this.number = number;
            this.coords = coords;
            this.pathFind = pathFind;
            this.cellList = cellList;
            ((CellNode)cellList[coords]).CupEnter(number);
            spriteRenderer.color = config.Colors[number];
        }

        public void Move(Vector2Int newCoords)
        {
            Deselect();
            isMoving = true;
            Vector2Int prevNode = ((CellNode)cellList[newCoords]).FromPath;
            
            List<Vector3> Steps = new List<Vector3>();
            Steps.Add((Vector3)(Vector2)prevNode);

            int fuse = 0;
            while (prevNode != coords && fuse < 1000)
            {
                fuse++;
                Steps.Add((Vector3)(Vector2)prevNode);
                prevNode = ((CellNode)cellList[prevNode]).FromPath;
            }

            Sequence sequence = DOTween.Sequence();
            sequence.SetAutoKill(true);
            sequence.onComplete = () => 
            { 
                isMoving = false;
                ((CellNode)cellList[coords]).CupEnter(number);
            };

            for (int i = Steps.Count - 1; i > 0; i--)
            {
                sequence.Append(transform.DOMove(Steps[i] + Vector3.back, 0.5f));
            }

            ((CellNode)cellList[coords]).CupExit();
            coords = newCoords;
            
            sequence.Append(transform.DOMove(new Vector3(coords.x, coords.y, -1), 0.5f));
            sequence.Play();
        }
    }
}
