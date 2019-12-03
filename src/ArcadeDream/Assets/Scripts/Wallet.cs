using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements player currency and tickets
/// Author: Jared Anderson
/// Version: 2
/// </summary>
public class Wallet : MonoBehaviour
{
    public float StealRatio = 0.33f;

    public Text cashText;
    public Text TicketsText;
    public Text ToiletTicketsText;

    public AudioSource flushSound;

    private int cash_m;
    private int tickets_m;
    private int toiletTickets_m;

    // Start is called before the first frame update
    void Start()
    {
        // Open the config file, decrypt it, and retain the cash, tickets, and toilet tickets.
        byte[] lines;
        ADIOManager.GetConfigFileContents(out lines);
        ADIOManager.DecryptThis(ref lines);
        var decrypted = Encoding.UTF8.GetString(lines);
        string[] decryptedLines = decrypted.Split('\n');
        string cashString = decryptedLines[3].Split('=')[1];
        string ticketsString = decryptedLines[4].Split('=')[1];
        string toilet_ticketsString = decryptedLines[5].Split('=')[1];
        // Convert each string to integers and store them in memory.
        cash_m = System.Int32.Parse(cashString);
        tickets_m = System.Int32.Parse(ticketsString);
        toiletTickets_m = System.Int32.Parse(toilet_ticketsString);
        // Initialize the UI text.
        cashText.text = cashString;
        TicketsText.text = ticketsString;
        ToiletTicketsText.text = toilet_ticketsString;
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
        //Playing flush sound
        if (!flushSound.isPlaying)
        {
            flushSound.Play();
        }
    }

    public void RetrieveTickets()
    {
        tickets_m = tickets_m + toiletTickets_m;
        toiletTickets_m = 0;
        SaveWallet();
    }

    private void SaveWallet()
    {
        // Open the config file and decrypt it.
        byte[] lines;
        ADIOManager.GetConfigFileContents(out lines);
        ADIOManager.DecryptThis(ref lines);
        var decrypted = Encoding.UTF8.GetString(lines);
        string[] decryptedLines = decrypted.Split('\n');
        // We don't need to make any changes to these variables, so just convert them back into the proper format.
        uint userID = System.Convert.ToUInt32(decryptedLines[0].Split('=')[1]);
        ulong permissions = System.Convert.ToUInt64(decryptedLines[1].Split('=')[1]);
        ulong equipment = System.Convert.ToUInt64(decryptedLines[2].Split('=')[1]);
        // Update these variables.
        uint cash = System.Convert.ToUInt32(cash_m);
        uint tickets = System.Convert.ToUInt32(tickets_m);
        uint toilet_tickets = System.Convert.ToUInt32(toiletTickets_m);
        ADIOManager.UpdateConfigFileContents(userID, permissions, equipment, cash, tickets, toilet_tickets);
        // Update the UI text.
        cashText.text = cash_m.ToString();
        TicketsText.text = tickets_m.ToString();
        ToiletTicketsText.text = "Stored Tickets: " + toiletTickets_m.ToString();
    }
}
