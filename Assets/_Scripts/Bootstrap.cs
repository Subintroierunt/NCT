using Entities;
using GameSystems;
using StaticData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CreateGrid createGrid;
    [SerializeField] private CreateCup createCup;
    [SerializeField] private GridPathFind pathFind;

    private GridData gridData;

    private void Start()
    {
        StartCoroutine(LoadConfig());
    }

    private void Init()
    {
        createGrid.Init(gridData);
        createCup.Init(createGrid.CellList, gridData);
        pathFind.Init(createGrid.CellList);
    }

    private IEnumerator LoadConfig()
    {
        LoadConfig loadConfig = new LoadConfig();
        gridData = loadConfig.Load();

        while (gridData.Equals(default(GridData)))
        {
            yield return null;
        }
        Init();
    }
}
