using Entities;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystems
{
    public class CupMovement : MonoBehaviour
    {
        private List<Cup> cupList = new List<Cup>();
        private List<PathGhost> pathGhosts = new List<PathGhost>();
        private Cup activeCup;

        public void AddCup(Cup cup)
        {
            cupList.Add(cup);
            cup.CupSelected += SetActiveCup;
        }

        public void AddPathGhost(PathGhost pathGhost)
        {
            pathGhosts.Add(pathGhost);
            pathGhost.PathSelected += MoveCup;
        }

        private void SetActiveCup(Cup cup)
        {
            if (activeCup != null)
            {
                activeCup.Deselect();
            }
            activeCup = cup;
            activeCup.Select();
        }
            

        private void MoveCup(Vector2Int target)
        {
            activeCup.Move(target);
        }

        private void OnDestroy()
        {
            foreach (PathGhost ghost in pathGhosts)
            {
                ghost.PathSelected -= MoveCup;
            }

            foreach (Cup cup in cupList)
            {
                cup.CupSelected -= SetActiveCup;
            }
        }
    }
}
