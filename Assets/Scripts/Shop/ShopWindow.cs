using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> shopInventorySlots;
    [SerializeField] private TextMeshProUGUI itemsValueText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var totalPrice = 0;
        foreach (InventorySlot slot in shopInventorySlots)
        {
            print(slot);
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Item selectedItem = itemInSlot.item;
                if (selectedItem.sellable)
                {
                    totalPrice += selectedItem.sellPrice * itemInSlot.count;
                }
            }
        }

        itemsValueText.text = totalPrice.ToString();
    }

    public void Sell()
    {
        var totalPrice = 0;
        foreach (InventorySlot slot in shopInventorySlots)
        {
            print(slot);
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Item selectedItem = itemInSlot.item;
                if (selectedItem.sellable)
                {
                    totalPrice += selectedItem.sellPrice * itemInSlot.count;
                
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
        
        FinancialManager.instance.DepositMoney(totalPrice);
    }

    public void MoveItemsBackToInventory()
    {
        foreach (InventorySlot slot in shopInventorySlots)
        {
            print(slot);
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Item selectedItem = itemInSlot.item;

                for (int i = 0; i < itemInSlot.count; i++)
                {
                    InventoryManager.instance.AddItem(selectedItem);
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
    }
}
