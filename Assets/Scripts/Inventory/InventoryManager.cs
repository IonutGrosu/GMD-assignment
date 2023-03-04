using System;
using StarterAssets;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    
    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;

    public Item[] startItems;

    public const int MAXSTACKSIZE = 64;

    private int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChageSelectedSlot(0);
        foreach (Item item in startItems)
        {
            AddItem(item);
        }
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isnumber = int.TryParse(Input.inputString, out int number);
            if (isnumber && number > 0 && number < 8)
            {
                ChageSelectedSlot(number-1);
            }
        }
    }

    void ChageSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            InventorySlots[selectedSlot].Deselect();
        }
        InventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        // check for a slot with count < max stack size
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < MAXSTACKSIZE && item.stackable )
            {
                itemInSlot.count++;
                itemInSlot.refreshCount();
                return true;
            }
        }
        
        // check for an empty slot
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) 
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool useItem)
    {
        InventorySlot slot = InventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item selectedItem = itemInSlot.item;
            if (useItem)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.refreshCount();
                }
            }
            return selectedItem;
        }

        return null;
    }
}
