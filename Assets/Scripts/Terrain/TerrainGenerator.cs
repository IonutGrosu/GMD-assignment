using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    [FormerlySerializedAs("GameWorldManager")] public GameWorldManager gameWorldManager;
    
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
    
    private int _size;

    private void Awake()
    {
        _size = gameWorldManager.worldSize;
    }

    public void GenerateTerrain(out Cell[,] grid)
    {
        var noiseMap = new float[_size, _size];
        var (xOffset, yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                var noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        var falloffMap = new float[_size, _size];
        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                var xv = x / (float)_size * 2 - 1;
                var yv = y / (float)_size * 2 - 1;
                var v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[_size, _size];
        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                var noiseValue = noiseMap[x, y];
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

                var cell = new Cell(cellType);
                grid[x, y] = cell;
            }
        }

        InstantiateTerrain(grid);
    }

    private void InstantiateTerrain(Cell[,] grid)
    {
        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                var cell = grid[x, y];
                
                if (cell.CellType.Equals(CellType.GRASS))
                {
                    Instantiate(grassTerrainPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    grassTerrainPrefab.tag = "Selectable";
                }
                else if (cell.CellType.Equals(CellType.SAND))
                {
                    Instantiate(sandTerrainPrefab, new Vector3(x, 0, y), Quaternion.identity);
                    sandTerrainPrefab.tag = "Selectable";
                }
            }
        }
    }
}