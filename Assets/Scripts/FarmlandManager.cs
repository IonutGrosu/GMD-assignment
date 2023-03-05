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
                    Vector3 position = go.transform.position;
                    Destroy(go);
                    Instantiate(dirtCubePrefab, position, Quaternion.identity);
                } else if (selectedItem.name.Equals("TomatoSeed"))
                {
                    // tag the dirt block with "PLANTED" or smth so you cannot plant multiple seeds on the same block
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
                }
            }
        }
    }
}
