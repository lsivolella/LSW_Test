using System;
using UnityEngine;

/// <summary>
/// Money wallet configuration scriptable object
/// </summary>
[CreateAssetMenu (menuName = "Wallet")]
public class WalletSO : ScriptableObject
{
    [SerializeField] int balance;

    public event Action<int> onTransactionMade;

    public int Balance { get { return balance; } }

    public void AddToWallet(int amount)
    {
        balance += amount;
        OnTransationMade(balance);
    }

    public bool RemoveFromWallet(int amount)
    {
        if (balance < amount) return false;

        balance -= amount;
        OnTransationMade(balance);
        return true;
    }

    private void OnTransationMade(int balance)
    {
        if (onTransactionMade == null) return;

        onTransactionMade(balance);
    }
}
