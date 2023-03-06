using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TomatoPlant : MonoBehaviour
{
    [SerializeField] private GameObject[] growthStagePrefabs;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private int timeToGrow;
    [SerializeField] private Tag harvestableTag;

    private int currentGrowthStage = 1;
    private bool fullyGrown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Grow());
    }

    private void OnDisable()
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
            currentGrowthStage++;
        }
        
        fullyGrown = true;
        this.gameObject.GetComponent<Tags>().AddTag(harvestableTag);
    }

    public int Harvest()
    {
        Destroy(this.gameObject);
        return GetSeedsOnHarvest();
    }

    private int GetSeedsOnHarvest()
    {
        return Random.Range(0, 3);
    }
}
