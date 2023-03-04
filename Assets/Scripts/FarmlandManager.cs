using System;
using StarterAssets;
using UnityEngine;

public class FarmlandManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dirtCubePrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Item selectedItem = InventoryManager.instance.GetSelectedItem(false);
            if (selectedItem != null)
            {
                if (selectedItem.name.Equals("Shovel"))
                {
                    GameObject go = Selectable.instance.GetSelection();
                    Vector3 position = go.transform.position;
                    Destroy(go);
                    Instantiate(dirtCubePrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
