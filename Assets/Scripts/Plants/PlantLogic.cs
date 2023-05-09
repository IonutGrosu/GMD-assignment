using System.Collections;
using StarterAssets.Plants;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] growthStagePrefabs;
    [SerializeField] private int timeToGrow;
    [SerializeField] private Plant plant;
    
    void Start()
    {
        StartCoroutine(Grow());
    }

    private void OnDisable()
    {
        StopCoroutine(Grow());
    }

    private void OnDestroy()
    {
        StopCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        for (int i = 0; i < growthStagePrefabs.Length-1; i++)
        {
            yield return new WaitForSeconds(timeToGrow);
            growthStagePrefabs[i].gameObject.SetActive(false);
            growthStagePrefabs[i+1].gameObject.SetActive(true);
        }
    }

    public Harvest Harvest()
    {
        Destroy(this.gameObject);
        return GetHarvestedSeedsAndFruits();
    }

    private Harvest GetHarvestedSeedsAndFruits()
    {
        var harvest = new Harvest(plant.plantType);
        if (plant.maxHarvestableSeeds > 0)
        {
            harvest.HarvestedSeeds = Random.Range(plant.minHarvestableSeeds, plant.maxHarvestableSeeds);
        }
        else harvest.HarvestedSeeds = 0;

        if (plant.maxHarvestableFruits > 0)
        {
            harvest.HarvestedFruits = Random.Range(plant.minHarvestableFruits, plant.maxHarvestableFruits);
        }
        else harvest.HarvestedFruits = 0;

        return harvest;
    }
}
