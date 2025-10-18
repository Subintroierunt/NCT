using Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems
{
    public class WinCondition : MonoBehaviour
    {
        private List<CellNode> nodes = new List<CellNode>();

        public void AddNodes(CellNode node)
        {
            nodes.Add(node);
            node.CupEntered += WinCheck;
        }

        private void WinCheck()
        {
            foreach (CellNode node in nodes)
            {
                if (node.IsMatch == false)
                {
                    return;
                }
            }
            SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            foreach (CellNode node in nodes)
            {
                node.CupEntered -= WinCheck;
            }
        }
    }
}
