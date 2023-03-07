using UnityEngine;

public class ShopClickToShow : MonoBehaviour
{
    [SerializeField] private GameObject inventoryGroup;
    [SerializeField] private GameObject inventoryWindow;
    [SerializeField] private GameObject shopGroup;
    [SerializeField] private ShopWindow shopWindow; 


    private void OnMouseDown()
    {
        ShowShopView();
    }

    public void ShowShopView()
    {
        print("clicked the shop");
        inventoryGroup.SetActive(true);
        inventoryWindow.transform.position += new Vector3(-300, 0, 0);
        shopGroup.SetActive(true);
    }

    public void HideShopView()
    {
        inventoryGroup.SetActive(false);
        inventoryWindow.transform.position += new Vector3(300, 0, 0);
        shopGroup.SetActive(false);
        
        shopWindow.MoveItemsBackToInventory();
    }
}
