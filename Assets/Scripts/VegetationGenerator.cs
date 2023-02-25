using UnityEngine;
using Random = UnityEngine.Random;

public class VegetationGenerator : MonoBehaviour
{
    public GameWorldManager GameWorldManager;

    [SerializeField]
    private float treeNoiseScale = .01f;
    [SerializeField]
    private float treeDensity = .35f;
    [SerializeField]
    private GameObject[] treePrefabs;
    
    private int size;

    private void Awake()
    {
        size = GameWorldManager.WorldSize;
    }
    
    
    public void GenerateTrees(Cell[,] grid)
    {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
                noiseMap[x, y] = noiseValue;
                print(noiseValue);
            }
        }

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.CellType.Equals(CellType.GRASS))
                {
                    float v = Random.Range(0f, treeDensity);
                    if (noiseMap[x, y] < v)
                    {
                        //tree here
                        GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                        GameObject tree = Instantiate(treePrefab, transform);
                        tree.transform.position = new Vector3(x, 0, y);
                        tree.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
                        tree.transform.localScale = Vector3.one*Random.Range(.8f, 1.2f);
                    }
                }
            }
        }
    }
    
}
