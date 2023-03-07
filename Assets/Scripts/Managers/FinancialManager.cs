using TMPro;
using UnityEngine;

public class FinancialManager : MonoBehaviour
{
    public static FinancialManager instance;
    
    public int money = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // RefreshMoneyText();
    }

    public void DepositMoney(int amount)
    {
        money += amount;
        RefreshMoneyText();
    }

    private void RefreshMoneyText()
    {
        moneyText.text = money.ToString() + " $";
    }
}
