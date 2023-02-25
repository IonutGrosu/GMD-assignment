using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    
    public Cell[,] WorldGrid;
    public int WorldSize = 100;

    public TerrainGenerator TerrainGenerator;
    public VegetationGenerator VegetationGenerator;

    private void Start()
    {
        TerrainGenerator.GenerateTerrain(out WorldGrid);
        VegetationGenerator.GenerateTrees(WorldGrid);
    }
}
