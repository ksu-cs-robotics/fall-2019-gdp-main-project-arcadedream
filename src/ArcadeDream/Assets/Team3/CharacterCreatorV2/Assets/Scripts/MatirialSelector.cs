using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MatirialSelector : MonoBehaviour
{
    public GameObject player;
    public Material skins; //skin color array (changed inunity editor)
    public Material[] shirts; //shirt color array (changed in unity editor)
    private Material[] loadout = new Material[2]; //loadout of custom items
    private SkinnedMeshRenderer MeshSkin; //element grabbing skinned mesh renderer
    private float red, blue, green; //floats for skin color
    public Slider sred, sblue, sgreen; //sliders for skin color
    public int skincount = 0;
    public int shirtcount = -1;
    // Create some asset folders.
    string prefabPath = "Assets/Prefabs/Player01.prefab";
    // Start is called before the first frame update
    void Start()
    {
        MeshSkin = GetComponent<SkinnedMeshRenderer>(); //testing
        loadout[0] = skins; //set the skin material

    }

    // Update is called once per frame
    void Update()
    {
        red = sred.value;
        blue = sblue.value;
        green = sgreen.value;
        skins.SetColor("_Color", new Color(red, green, blue, 0f)); //set the skin color

        if (Input.GetKeyDown("d")) //increment shirt count and save to loadout
        {
            shirtcount++;
            if (shirtcount >= shirts.Length)
            {
                shirtcount = 0;
            }
            loadout[1] = shirts[shirtcount];
            MeshSkin.materials = loadout;
        }

        if (Input.GetKeyDown("a")) //increment shirt count and save to loadout
        {
            shirtcount--;
            if (shirtcount < 0)
            {
                shirtcount = shirts.Length - 1;
            }
            loadout[1] = shirts[shirtcount];
            MeshSkin.materials = loadout;
        }

        if (Input.GetKeyDown("l")) //save player object as prefab for saving
        {
            PrefabUtility.SaveAsPrefabAsset(player, prefabPath);
        }
    }
}
