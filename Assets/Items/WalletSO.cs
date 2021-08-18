using UnityEngine;

[CreateAssetMenu (menuName = "Wallet")]
public class WalletSO : ScriptableObject
{
    [SerializeField] int balance;

    public int Balance { get { return balance; } }

    public void AddToWallet(int amount)
    {
        balance += amount;
    }

    public bool RemoveFromWallet(int amount)
    {
        if (balance < amount) return false;

        balance -= amount;
        return true;
    }
}
