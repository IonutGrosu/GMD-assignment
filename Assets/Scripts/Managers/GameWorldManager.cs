using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameWorldManager : MonoBehaviour
{
    private Cell[,] _worldGrid;
    [FormerlySerializedAs("WorldSize")] public int worldSize = 100;

    [FormerlySerializedAs("TerrainGenerator")] public TerrainGenerator terrainGenerator;
    [FormerlySerializedAs("VegetationGenerator")] public VegetationGenerator vegetationGenerator;
    [FormerlySerializedAs("PlayerSpawnHelper")] public PlayerSpawnHelper playerSpawnHelper;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        terrainGenerator.GenerateTerrain(out _worldGrid);
        vegetationGenerator.GenerateTrees(_worldGrid);
        playerSpawnHelper.SpawnPlayer();
    }
}
