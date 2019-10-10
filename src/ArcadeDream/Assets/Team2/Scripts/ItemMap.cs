using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
/// Notes: Inherits from MonoBehaviour to allow for more predictable behaviour in game...
/// </summary>
public class ItemMap : MonoBehaviour
{
    public List<Item> IDMap;
    private static readonly uint _mapSize = 32;

    [TextArea] public string Description1;
    [SerializeField] GameObject ID0, ID1, ID2, ID3, ID4, ID5, ID6, ID7;

    [TextArea] public string Description2;
    [SerializeField] GameObject ID8, ID9, ID10, ID11, ID12, ID13, ID14, ID15;

    [TextArea] public string Description3;
    [SerializeField] GameObject ID16, ID17, ID18, ID19, ID20, ID21, ID22, ID23;

    [TextArea] public string Description4;
    [SerializeField] GameObject ID24, ID25, ID26, ID27, ID28, ID29, ID30, ID31;

    #region ** Bitwise Method Implementations **
    /// <summary>
    /// Takes in both a full db hash, and 2^item ID, Logical AND's them, and returns the resulting value.
    /// Author: Josh Dotson
    /// Version: 2
    /// Notes: Was previously "GetPermission()"
    /// </summary>
    /// <param name="hash">Unsigned 32 Bit int from Database</param>
    /// <param name="itemID">ID of the Item in Question</param>
    /// <returns>Result of a & operation between hash and itemID, casted as a boolean</returns>
    public static bool PopBit(uint hash, uint itemID) { return Convert.ToBoolean(hash & ((uint)Math.Pow(2, itemID))); }

    /// <summary>
    /// Takes in both a full db hash, and an 2^item ID, Logical OR's them, and returns the resulting value.
    /// Author: Josh Dotson
    /// Version: 1
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="itemID"></param>
    /// <returns>The resulting int of itemID being OR'd into permissionsHash</returns>
    public static int PushBit(uint hash, uint itemID) { return hash | ((uint)Math.Pow(2, itemID)) }

    /// <summary>
    /// Takes in both a full db hash, and an 2^item ID, Logical XOR's them, and returns the resulting value.
    /// Author: Josh Dotson
    /// Version: 1
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="itemID"></param>
    /// <returns>The resulting int of itemID being OR'd into permissionsHash</returns>
    public static int FlipBit(uint hash, uint itemID) { return hash ^ ((uint)Math.Pow(2, itemID)) }
    #endregion

    /// <summary>
    /// Takes an equipment hash from the database, and returns a list of equipped gameObjects based on it
    /// Author: Josh Dotson
    /// Version: 1
    /// </summary>
    /// <param name="equipmentHash">Unsigned 32 Bit int from Database</param>
    /// <returns>List of gameObjects the Player Should Be Wearing</returns>
    public static List<GameObject> GetEquippedItems(uint equipmentHash)
    {
        // ...
        
        return new List<GameObject>(); // Placeholder
    }

    public void Awake()
    {
        // Initialize the map list
        IDMap = new List<Item>();

        // This uses reflection to avoid typing out all of the variable names
        for (var i = 0; i <= _mapSize - 1; ++i)
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
