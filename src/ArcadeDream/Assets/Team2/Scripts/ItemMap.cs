using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// Main class used in encrypting/decrypting config, and wallet data (Retired)
/// Author(s): Josh Dotson
/// Version: 1
/// </summary>
/*public class ADRSAServiceProvider : IDisposable
{
    // make this private in the future, and configure our own methods for it
    private RSACryptoServiceProvider ADRSA;

    public ADRSAServiceProvider() { ADRSA = new RSACryptoServiceProvider(); }
    public ADRSAServiceProvider(string xmlString) : this() { InitializeFromXML(xmlString); }

    // Generates a new set of RSA parameters for first time users
    public void GenerateRSAParameters() { ADRSA.ExportParameters(true); }
    // Initializes parameters from XML file
    public void InitializeFromXML(string xmlString) { ADRSA.FromXmlString(xmlString); }

    public byte[] Encrypt(byte[] data) { return ADRSA.Encrypt(data, false); }
    public byte[] Decrypt(byte[] data)
    {
        try
        {
            return ADRSA.Decrypt(data, false);
        }
        catch (Exception ex)
        {
            var test = ex;
            return default(byte[]);
        }
    }

    // C# destructor
    public void Dispose()
    {
        ADRSA.Dispose();
    }
} */

/// <summary>
/// Manages all IO operations between .arcadedream files, and this program
/// Author: Josh Dotson
/// Version: 1
/// </summary>
public static class ADIOManager
{
    // Const map to the config file location on the player's local machine
    private static readonly string _configFilePath =    $@"{Environment.CurrentDirectory}\config.arcadedream";
    private static readonly string _certFilePath =      $@"{Environment.CurrentDirectory}\adcert.arcadedream";

    public static bool GenerateNewConfigFile()
    {
        try
        {
            // Create the new config file...
            File.Create(_configFilePath).Dispose();
            // Generate a new default set of contents for it...
            string[] contents = { "#userID", "#permissions=0", "#equipment=0", "#cash=0", "#tickets=0", "#toilet_tickets=0" };
            File.WriteAllLines(_configFilePath, contents, System.Text.Encoding.UTF8);
            // And return success!
            return true;
        }
        catch { return false; }
    }
    public static bool GenerateNewCertFile()
    {
        try
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                File.Create(_certFilePath).Dispose();
                string contents = rsa.ToXmlString(true);
                File.WriteAllText(_certFilePath, contents);
            }

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
    public static bool GetConfigFileContents(out string lines)
    {
        try
        {
            /*using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(File.ReadAllText(_certFilePath));

                // Get the contents, and then decrypt them into lines
                var contents = File.ReadAllBytes(_configFilePath);
                lines = Encoding.UTF8.GetString(RSA.Decrypt(contents, false));
            }*/

            lines = File.ReadAllText(_configFilePath, System.Text.Encoding.UTF8);
            return true;
        }
        catch
        {
            lines = default(string);
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

    public static bool UpdateConfigFileContents(uint userID, ulong permissions, ulong equipment, uint cash, uint tickets, uint toilet_tickets)
    {
        try
        {
            // Clear the contents of the file...
            File.WriteAllText(_configFilePath, string.Empty);
            // Generate the new contents...
            string contents =
                $"#userID={userID}" +
                $"#permissions={permissions}" +
                $"#equipment={equipment}" +
                $"#cash={cash}" +
                $"#tickets={tickets}" +
                $"#toilet_tickets={toilet_tickets}";

            // Encrypt those new contents
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.FromXmlString(File.ReadAllText(_certFilePath));
                return UpdateConfigFileContents(RSA.Encrypt(Encoding.UTF8.GetBytes(contents), false));
            }
        }
        catch { return false; }
    }
    public static bool UpdateConfigFileContents(byte[] bytes)
    {
        try
        {
            // Clear file contents...
            File.WriteAllText(_configFilePath, string.Empty);
            // And write the new bytes to the file
            File.WriteAllBytes(_configFilePath, bytes);

            return true;
        }
        catch { return false; }
    }

    // Takes some data by reference, and encrypts/decrypts it, returning true on success.
    public static bool EncryptThis(ref byte[] data)
    {
        try
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                string contents;
                if (GetCertFileContents(out contents))
                {
                    RSA.FromXmlString(contents);
                    // Decrypt the data
                    data = RSA.Encrypt(data, false);
                }
            }
            // Return success!
            return true;
        }
        catch { return false; }
    }
    public static byte[] EncryptThis(byte[] data)
    {
        try
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                string contents;
                if (!GetCertFileContents(out contents))
                    throw new Exception("Could not open certificate file for reading");
                
                RSA.FromXmlString(contents);
                // Decrypt the data
                return RSA.Encrypt(data, false);
            }
        }
        catch { return default(byte[]); }
    }
    public static bool DecryptThis(ref byte[] data)
    {
        try
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                string contents;
                if (GetCertFileContents(out contents))
                {
                    RSA.FromXmlString(contents);
                    // Decrypt the data
                    data = RSA.Decrypt(data, false);
                }
            }
            // Return success!
            return true;
        }
        catch { return false; }
    }
    public static byte[] DecryptThis(byte[] data)
    {
        try
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                string contents;
                if (!GetCertFileContents(out contents))
                    throw new Exception("Could not open certificate file for reading");

                RSA.FromXmlString(contents);
                // Decrypt the data
                return RSA.Decrypt(data, false);
            }
        }
        catch { return default(byte[]); }
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
    private const uint _mapSize = 64; // BITS
    private const uint _typeNumber = 4;

    // Tells ItemMap where to segment a _mapSize bit hash for the resulting sub-hashs to encompass exactly 1 type of item
    private static readonly uint[] _typeBitShiftMap = new uint[/*_typeNumber*/]
    {
        // By default, there are 4 sections (types) in ItemMap, each having 16 items in total. These may be changed here...
        0x000f, 0x001f, 0x002f, 0x003f
    };

    [SerializeField] Material[] ItemIDMap = new Material[_mapSize];

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
    public List<Item> IsUnlocked(ulong permissionsHash)
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
        // IDMap = new List<Item>();

        // This uses reflection to avoid typing out all of the variable names
        /* for (var i = 0; i < _mapSize; ++i)
        {
            try // In case some of the ID#s don't hold references to anything
            {
                Item item = new Item(i); // Create a new Item with an ID
                item.item = (GameObject)typeof(ItemMap).GetField($"ID{i}").GetValue(this); // Using reflection, set the item reference

                // IDMap.Add(item);
            }
            catch { continue; }
        }*/
    }
}
