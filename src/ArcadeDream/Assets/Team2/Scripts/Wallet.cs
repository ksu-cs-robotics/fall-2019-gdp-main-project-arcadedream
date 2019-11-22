using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements player currency and tickets
/// Author: Jared Anderson
/// Version: 1
/// </summary>
public class Wallet : MonoBehaviour
{
    public float StealRatio = 0.33f;

    public Text cashText;
    public Text TicketsText;
    public Text ToiletTicketsText;

    private const string filepath_m = "./money.txt";
    private int cash_m;
    private int tickets_m;
    private int toiletTickets_m;

    // Start is called before the first frame update
    void Start()
    {
        string[] wallet = File.ReadAllLines(filepath_m);
        cash_m = System.Int32.Parse(wallet[0]);
        tickets_m = System.Int32.Parse(wallet[1]);
        toiletTickets_m = System.Int32.Parse(wallet[2]);
        // Initialize the UI text.
        cashText.text = wallet[0];
        TicketsText.text = wallet[1];
        ToiletTicketsText.text = wallet[2];
    }

    public void SpendCash(int amount)
    {
        cash_m = cash_m - amount;
        SaveWallet();
    }

    public void GainCash(int amount)
    {
        cash_m = cash_m + amount;
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

    public void StealTickets()
    {
        int stolenTickets = (int)(System.Math.Floor(StealRatio * tickets_m));
        tickets_m = tickets_m - stolenTickets;
        SaveWallet();
    }

    public void StoreTickets()
    {
        toiletTickets_m = toiletTickets_m + tickets_m;
        tickets_m = 0;
        SaveWallet();
    }

    public void RetrieveTickets()
    {
        tickets_m = tickets_m + toiletTickets_m;
        toiletTickets_m = 0;
        SaveWallet();
    }

    private void SaveWallet()
    {
        string[] wallet = new string[3];
        wallet[0] = cash_m.ToString();
        wallet[1] = tickets_m.ToString();
        wallet[2] = toiletTickets_m.ToString();
        File.WriteAllLines(filepath_m, wallet);
        // Update the UI text.
        cashText.text = wallet[0];
        TicketsText.text = wallet[1];
        ToiletTicketsText.text = "Stored Tickets: " + wallet[2];
    }
}
