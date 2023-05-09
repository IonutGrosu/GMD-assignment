using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class VegetationGenerator : MonoBehaviour
{
    [FormerlySerializedAs("GameWorldManager")] public GameWorldManager gameWorldManager;

    [SerializeField]
    private float treeNoiseScale = .01f;
    [SerializeField]
    private float treeDensity = .35f;
    [SerializeField]
    private GameObject[] treePrefabs;
    
    [FormerlySerializedAs("PlayerSpawnHelper")] public PlayerSpawnHelper playerSpawnHelper;

    [FormerlySerializedAs("WildlifeSpawner")] public WildlifeSpawner wildlifeSpawner;
    
    private int _size;

    private void Awake()
    {
        _size = gameWorldManager.worldSize;
    }
    
    
    public void GenerateTrees(Cell[,] grid)
    {
        var noiseMap = new float[_size, _size];
        var (xOffset, yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                var noiseValue = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        for (var y = 0; y < _size; y++)
        {
            for (var x = 0; x < _size; x++)
            {
                var cell = grid[x, y];
                if (cell.CellType.Equals(CellType.GRASS))
                {
                    var v = Random.Range(0f, treeDensity);
                    if (noiseMap[x, y] < v)
                    {
                        //tree here
                        GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                        GameObject tree = Instantiate(treePrefab, transform);
                        treePrefab.tag = "Selectable";
                        tree.transform.position = new Vector3(x, 0, y);
                        tree.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                        tree.transform.localScale = Vector3.one*Random.Range(.8f, 1.2f);
                    }
                }
            }
        }
        
        playerSpawnHelper.SpawnPlayer();
        wildlifeSpawner.SpawnChickens();
    }
    
}
