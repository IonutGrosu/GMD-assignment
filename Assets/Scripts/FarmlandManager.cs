using StarterAssets;
using UnityEngine;

public class FarmlandManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dirtCubePrefab;

    [SerializeField]
    private GameObject tomatoPlantPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Item selectedItem = InventoryManager.instance.GetSelectedItem(false);
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
                            InventoryManager.instance.GetSelectedItem(true);
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
                            TomatoPlant tomatoPlant = go.GetComponentInParent<TomatoPlant>();
                            if (tomatoPlant != null)
                            {
                                int seedsHarvested = tomatoPlant.Harvest();
                                print($"Harvested {seedsHarvested} seeds from the tomato plant");
                            }
                        }
                    }
                }
            }
        }
    }
}
