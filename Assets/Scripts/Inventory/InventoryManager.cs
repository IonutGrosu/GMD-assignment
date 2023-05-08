using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using StarterAssets.Managers;
using UnityEngine;

public class InventoryManager : MonoBehaviour, ISaveManager
{
    public static InventoryManager Instance;
    
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public Item[] startItems;

    public const int MaxStackSize = 64;

    private int _selectedSlot = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
            ChangeSelectedSlot(0);
            foreach (var item in startItems)
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
                ChangeSelectedSlot(number-1);
            }
        }
    }

    private void ChangeSelectedSlot(int newValue)
    {
        if (_selectedSlot >= 0)
        {
            inventorySlots[_selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        _selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        // check for a slot with count < max stack size
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < MaxStackSize && item.stackable )
            {
                itemInSlot.count++;
                itemInSlot.refreshCount();
                return true;
            }
        }
        
        // check for an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
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
        InventorySlot slot = inventorySlots[_selectedSlot];
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

    public void LoadData(SaveItems data)
    {
        if (data == null) return;
        for(var i =0; i< data.Items.Length; i++)
        {
            for (var j = 0; j < data.Counts[i]; j++)
            {
                AddItem(data.Items[i]);
            }
        }
    }

    public void SaveData(SaveItems data)
    {
        var items = new List<Item>();
        var counts = new List<int>();
        foreach (var item in inventorySlots)
        {
            var a = item.GetComponentInChildren<InventoryItem>();
            if (!a || a.item == null) continue;
            items.Add(a.item);
            counts.Add(a.count);
        }
        
        data.Items = items.ToArray();
        data.Coins = 0;
        data.Counts = counts.ToArray();
    }
}
