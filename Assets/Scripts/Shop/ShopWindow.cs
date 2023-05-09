using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> shopInventorySlots;
    [SerializeField] private TextMeshProUGUI itemsValueText;

    // Update is called once per frame
    void Update()
    {
        var totalPrice = 0;
        foreach (var slot in shopInventorySlots)
        {
            print(slot);
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                var selectedItem = itemInSlot.item;
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
        foreach (var slot in shopInventorySlots)
        {
            print(slot);
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                var selectedItem = itemInSlot.item;
                if (selectedItem.sellable)
                {
                    totalPrice += selectedItem.sellPrice * itemInSlot.count;
                
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
        
        FinancialManager.Instance.DepositMoney(totalPrice);
    }

    public void MoveItemsBackToInventory()
    {
        foreach (var slot in shopInventorySlots)
        {
            print(slot);
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                var selectedItem = itemInSlot.item;

                for (var i = 0; i < itemInSlot.count; i++)
                {
                    InventoryManager.Instance.AddItem(selectedItem);
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
    }
}
