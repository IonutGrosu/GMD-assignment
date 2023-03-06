using System;
using UnityEngine;

public class GameWorldManager : MonoBehaviour
{
    
    public Cell[,] WorldGrid;
    public int WorldSize = 100;

    public TerrainGenerator TerrainGenerator;
    public VegetationGenerator VegetationGenerator;
    public PlayerSpawnHelper PlayerSpawnHelper;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        TerrainGenerator.GenerateTerrain(out WorldGrid);
        VegetationGenerator.GenerateTrees(WorldGrid);
        // PlayerSpawnHelper.SpawnPlayer();
        
    }
}
