using UnityEngine;
using TMPro;

public class MoneyHudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.Player.Wallet.onTransactionMade += UpdateMoneyText;
        UpdateMoneyText(gameManager.Player.Wallet.Balance);
    }

    public void UpdateMoneyText(int amount)
    {
        moneyText.text = amount.ToString();
    }

    private void OnApplicationQuit()
    {
        gameManager.Player.Wallet.onTransactionMade -= UpdateMoneyText;
    }
}
