using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Implements player currency and tickets
/// Author: Jared Anderson
/// Version: 1
/// </summary>
public class Wallet : MonoBehaviour
{
    private const string filepath_m = "./money.txt";
    private int coins_m;
    private int tickets_m;

    // Start is called before the first frame update
    void Start()
    {
        string[] wallet = File.ReadAllLines(filepath_m);
        coins_m = System.Int32.Parse(wallet[0]);
        tickets_m = System.Int32.Parse(wallet[1]);
        Debug.Log("Coins: " + coins_m);
        Debug.Log("Tickets: " + tickets_m);
    }

    public void SpendCoins(int amount)
    {
        coins_m = coins_m - amount;
        SaveWallet();
    }

    public void GainCoins(int amount)
    {
        coins_m = coins_m + amount;
        SaveWallet();
    }

    public void SpendTickets(int amount)
    {
        tickets_m = tickets_m - amount;
        SaveWallet();
    }

    public void GainTickets(int amount)
    {
        tickets_m = tickets_m + amount;
        SaveWallet();
    }

    private void SaveWallet()
    {
        string[] wallet = new string[2];
        wallet[0] = coins_m.ToString();
        wallet[1] = tickets_m.ToString();
        File.WriteAllLines(filepath_m, wallet);
    }
}
