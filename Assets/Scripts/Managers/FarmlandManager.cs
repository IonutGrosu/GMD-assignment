using StarterAssets;
using StarterAssets.Plants;
using StarterAssets.Utils;
using UnityEngine;

public class FarmlandManager : MonoBehaviour
{
    [SerializeField] private GameObject dirtCubePrefab;

    [SerializeField] private GameObject tomatoPlantPrefab;
    [SerializeField] private GameObject eggplantPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Item selectedItem = InventoryManager.Instance.GetSelectedItem(false);
            if (selectedItem != null)
            {
                if (selectedItem.name.Equals("Shovel")) // also test if selected gameobject is grass
                {
                    GameObject go = Selectable.instance.GetSelection();
                    go.TryGetComponent(out Tags selectedGameObjectTags);
                    if (selectedGameObjectTags.HasTag("Grass"))
                    {
                        Vector3 position = go.transform.position;
                        Destroy(go);
                        Instantiate(dirtCubePrefab, position, Quaternion.identity);
                    }
                } else if (selectedItem.name.Equals("TomatoSeed"))
                {
                    GameObject go = Selectable.instance.GetSelection();
                    Vector3 position = go.transform.position;
                    if (go.TryGetComponent<Tags>(out var tags) && tags.HasTag("PlantableDirt"))
                    {
                        if (go.transform.childCount <= 1)  
                        {
                            GameObject tomatoPlant = Instantiate(tomatoPlantPrefab, position, Quaternion.identity);
                            tomatoPlant.transform.parent = go.transform;
                            InventoryManager.Instance.GetSelectedItem(true);
                        }
                    }
                } else if (selectedItem.name.Equals("EggplantSeed"))
                {
                    GameObject go = Selectable.instance.GetSelection();
                    Vector3 position = go.transform.position;
                    if (go.TryGetComponent<Tags>(out var tags) && tags.HasTag("PlantableDirt"))
                    {
                        if (go.transform.childCount <= 1)  // test if not already a planted plant :) 
                        {
                            GameObject eggplant = Instantiate(eggplantPrefab, position, Quaternion.identity);
                            eggplant.transform.parent = go.transform;
                            InventoryManager.Instance.GetSelectedItem(true);
                        }
                    }

                } else if(selectedItem.name.Equals("Pickaxe"))
                {
                    GameObject go = Selectable.instance.GetSelection();
                    Tags selectedGameObjectTags = go.GetComponent<Tags>();
                    if (selectedGameObjectTags != null)
                    {
                        if (selectedGameObjectTags.HasTag("Harvestable"))
                        {
                            PlantLogic plantLogic = go.GetComponentInParent<PlantLogic>();
                            if (plantLogic != null)
                            {
                                Harvest harvest = plantLogic.Harvest();
                                HarvestUtil.instance.AddHarvestToInventory(harvest);
                                print($"Harvested {harvest.HarvestedSeeds} seeds and {harvest.HarvestedFruits} fruits from the {harvest.PlantType} plant");
                            }
                        }
                    }
                }
            }
        }
    }
}
