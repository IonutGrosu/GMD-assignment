using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = InventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item not added");
        }
    }

    public void GetSelectedItem()
    {
        Item receivedItem = InventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item received.");
        }
    }
    
    public void UseSelectedItem()
    {
        Item receivedItem = InventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Received item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item received.");
        }
    }
}
