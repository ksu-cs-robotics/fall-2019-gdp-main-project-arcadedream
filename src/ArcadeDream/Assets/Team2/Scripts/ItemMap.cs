﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Main class used in encrypting/decrypting config, and wallet data
/// Author(s): Josh Dotson
/// Version: 1
/// </summary>
public class ADRSAServiceProvider : IDisposable
{
    // make this private in the future, and configure our own methods for it
    private RSACryptoServiceProvider ADRSA;

    public ADRSAServiceProvider()
    {
        ADRSA = new RSACryptoServiceProvider();
    }
    public ADRSAServiceProvider(string xmlString) : this() { InitializeFromXML(xmlString); }

    // Generates a new set of RSA parameters for first time users
    public void GenerateRSAParameters() { ADRSA.ExportParameters(true); }
    // Initializes parameters from XML file
    public void InitializeFromXML(string xmlString) { ADRSA.FromXmlString(xmlString); }

    // Placeholders for now
    public byte[] Encrypt(byte[] data) { return ADRSA.Encrypt(data, false); }
    public byte[] Decrypt(byte[] data) { return ADRSA.Decrypt(data, false); }

    // C# destructor
    public void Dispose()
    {
        ADRSA.Dispose();
    }
}

/// <summary>
/// Manages all IO operations between .arcadedream files, and this program
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public static class ADIOManager
{
    // Const map to the config file location on the player's local machine
    private static readonly string _configFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Arcade Dream\config.arcadedream";
    private static readonly string _certFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Arcade Dream\adcert.arcadedream";

    public static bool GenerateNewConfigFile()
    {
        try
        {
            File.Create(_configFilePath);
            return true;
        }
        catch { return false; }
    }
    public static bool GenerateNewCertFile()
    {
        try
        {
            File.Create(_certFilePath);
            return true;
        }
        catch { return false; }
    }

    public static bool GetConfigFileContents(out byte[] bytes)
    {
        try
        {
            bytes = File.ReadAllBytes(_configFilePath);
            return true;
        }
        catch
        {
            bytes = default(byte[]);
            return false;
        }
    }
    public static bool GetCertFileContents(out string xmlString)
    {
        try
        {
            xmlString = File.ReadAllText(_certFilePath);
            return true;
        }
        catch
        {
            xmlString = default(string);
            return false;
        }
    }
}


/// <summary>
/// Stores an Item gameObject along with its corresponding ID
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public class Item
{
    public int ID;
    public GameObject item;

    public Item()
    {
        ID = -1;
        item = default(GameObject);
    }
    public Item(int newID) : this()
    {
        ID = newID;
    }
}

/// <summary>
/// Maps database ids to actual gameObjects in game. Useful for equipable items.
/// Author: Josh Dotson
/// Version: 1
/// Notes: Inherits from MonoBehaviour to allow for more predictable behaviour in game, however may make static in future...
/// </summary>
public class ItemMap : MonoBehaviour
{
    public List<Item> IDMap;
    private static readonly uint _mapSize = 64; // BITS
    private const uint _typeNumber = 4;

    // Tells ItemMap where to segment a _mapSize bit hash for the resulting sub-hashs to encompass exactly 1 type of item
    private static readonly uint[] _typeBitShiftMap = new uint[4]
    {
        // By default, there are 4 sections (types) in ItemMap, each having 8 items in total. These may be changed here...
        0x000f, 0x001f, 0x002f, 0x003f
    };

    [TextArea] public string ItemType1Description;
    [SerializeField] Material ID0, ID1, ID2, ID3, ID4, ID5, ID6, ID7, ID8, ID9, ID10, ID11, ID12, ID13, ID14, ID15;

    [TextArea] public string ItemType2Description;
    [SerializeField] Material ID16, ID17, ID18, ID19, ID20, ID21, ID22, ID23, ID24, ID25, ID26, ID27, ID28, ID29, ID30, ID31;

    [TextArea] public string ItemType3Description;
    [SerializeField] Material ID32, ID33, ID34, ID35, ID36, ID37, ID38, ID39, ID40, ID41, ID42, ID43, ID44, ID45, ID46, ID47;

    [TextArea] public string ItemType4Description;
    [SerializeField] Material ID48, ID49, ID50, ID51, ID52, ID53, ID54, ID55, ID56, ID57, ID58, ID59, ID60, ID61, ID62, ID63;

    #region ** Bitwise Method Implementations **
    /// <summary>
    /// Takes in both a full db hash, and 2^item ID, Logical AND's them, and returns the resulting value.
    /// Author: Josh Dotson
    /// Version: 2
    /// Notes: Was previously "GetPermission()"
    /// </summary>
    /// <param name="hash">Unsigned 64 Bit int from Database</param>
    /// <param name="itemID">ID of the Item in Question</param>
    /// <returns>Result of a & operation between hash and itemID, casted as a boolean</returns>
    public static bool PopBit(ulong hash, uint itemID) { return Convert.ToBoolean(hash & ((ulong)Math.Pow(2, itemID))); }

    /// <summary>
    /// Takes in both a full db hash, and an 2^item ID, Logical OR's them, and returns the resulting value.
    /// Author: Josh Dotson
    /// Version: 1
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="itemID"></param>
    /// <returns>The resulting int of itemID being OR'd into permissionsHash</returns>
    public static ulong PushBit(ulong hash, uint itemID) { return hash | ((ulong)Math.Pow(2, itemID)); }

    /// <summary>
    /// Takes in both a full db hash, and an 2^item ID, Logical XOR's them, and returns the resulting value.
    /// Author: Josh Dotson
    /// Version: 1
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="itemID"></param>
    /// <returns>The resulting int of itemID being OR'd into permissionsHash</returns>
    public static ulong FlipBit(ulong hash, uint itemID) { return hash ^ ((ulong)Math.Pow(2, itemID)); }
    #endregion

    #region ** Helpful Functions **
    /// <summary>
    /// Returns a list of all items that the user is allowed to use based on a database hash
    /// Author: Josh Dotson
    /// Version: 1
    /// Notes: Untested work in progress
    /// </summary>
    /// <param name="permissionsHash">Unsigned 64 Bit int from the database</param>
    /// <returns></returns>
    public List<Item> HasUnlocked(ulong permissionsHash)
    {
        List<Item> unlockedItems = new List<Item>();

        for (int i = 0; i < _mapSize; ++i)
        {
            // If the i'th bit in the permissionHash is 1
            if (((permissionsHash >> i) & 1) == 1)
            {
                try // In case some of the ID#s don't hold references to anything
                {
                    unlockedItems.Add(new Item(i));
                    unlockedItems[i].item = (GameObject)typeof(ItemMap).GetField($"ID{i}").GetValue(this); // Using reflection, set the item reference
                }
                catch { continue; }
            }
        }

        return unlockedItems;
    }
    /// <summary>
    /// Takes an equipment hash from the database, and returns a list of equipped gameObjects based on it
    /// Author: Josh Dotson
    /// Version: 1
    /// </summary>
    /// <param name="equipmentHash">Unsigned 32 Bit int from Database</param>
    /// <returns>List of gameObjects the Player Should Be Wearing</returns>
    public static List<GameObject> GetEquippedItems(ulong equipmentHash)
    {
        // ...

        return new List<GameObject>(); // Placeholder
    }
    #endregion

    public void Awake()
    {
        // Initialize the map list
        IDMap = new List<Item>();

        // This uses reflection to avoid typing out all of the variable names
        for (var i = 0; i < _mapSize; ++i)
        {
            try // In case some of the ID#s don't hold references to anything
            {
                Item item = new Item(i); // Create a new Item with an ID
                item.item = (GameObject)typeof(ItemMap).GetField($"ID{i}").GetValue(this); // Using reflection, set the item reference

                IDMap.Add(item);
            }
            catch { continue; }
        }
    }
}
