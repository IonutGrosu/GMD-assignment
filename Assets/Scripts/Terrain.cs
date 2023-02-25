using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Terrain : MonoBehaviour
{
    [SerializeField]
    private GameObject grassTerrainPrefab;
    [SerializeField]
    private GameObject sandTerrainPrefab;
    [SerializeField]
    private float waterLevel = .4f;
    [SerializeField]
    private float sandLevel = .6f;
    [SerializeField]
    private float scale = .1f;
    [SerializeField]
    private int size = 500;

    Cell[,] grid;

    void Start()
    {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
                print(noiseValue);
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
                // bool isWater = noiseValue < waterLevel;
                // Cell cell = new Cell(isWater);

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

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Cell cell = grid[x, y];
                print(cell.CellType);
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