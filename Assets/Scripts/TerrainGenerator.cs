using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public GameWorldManager GameWorldManager;
    
    [SerializeField]
    private GameObject grassTerrainPrefab;
    [SerializeField]
    private GameObject sandTerrainPrefab;
    [SerializeField]
    private float waterLevel = .25f;
    [SerializeField]
    private float sandLevel = .35f;
    [SerializeField]
    private float scale = .05f;
    
    private int size;

    private void Awake()
    {
        size = GameWorldManager.WorldSize;
    }

    public void GenerateTerrain(out Cell[,] grid)
    {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        float[,] falloffMap = new float[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = noiseMap[x, y];
                noiseValue -= falloffMap[x, y];

                CellType cellType;
                if (noiseValue < waterLevel)
                {
                    cellType = CellType.WATER;
                } else if (noiseValue < sandLevel)
                {
                    cellType = CellType.SAND;
                }
                else
                {
                    cellType = CellType.GRASS;
                }

                Cell cell = new Cell(cellType);
                grid[x, y] = cell;
            }
        }

        InstantiateTerrain(grid);
    }

    void InstantiateTerrain(Cell[,] grid)
    {

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                
                if (cell.CellType.Equals(CellType.GRASS))
                {
                    Instantiate(grassTerrainPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
                else if (cell.CellType.Equals(CellType.SAND))
                {
                    Instantiate(sandTerrainPrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
    }
}