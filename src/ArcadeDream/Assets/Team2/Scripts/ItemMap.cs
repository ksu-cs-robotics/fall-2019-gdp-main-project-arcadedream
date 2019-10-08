using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class ItemMap : MonoBehaviour
{
    public List<Item> idMap;

    // Can't comma these to make them neater, as the editor doesn't like that
    [TextArea] public string Description1;
    [SerializeField] GameObject id0, id1, id2, id3, id4, id5, id6, id7;

    [TextArea] public string Description2;
    [SerializeField] GameObject id8, id9, id10, id11, id12, id13, id14, id15;

    [TextArea] public string Description3;
    [SerializeField] GameObject id16, id17, id18, id19, id20, id21, id22, id23;

    [TextArea] public string Description4;
    [SerializeField] GameObject id24, id25,id26,id27,id28,id29,id30,id31;

    public void Start()
    {
        idMap = new List<Item>();
    }
}
