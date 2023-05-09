using TMPro;
using UnityEngine;

public class FinancialManager : MonoBehaviour
{
    public static FinancialManager Instance;
    
    public int money = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void DepositMoney(int amount)
    {
        money += amount;
        RefreshMoneyText();
    }

    private void RefreshMoneyText()
    {
        moneyText.text = money + " $";
    }
}
