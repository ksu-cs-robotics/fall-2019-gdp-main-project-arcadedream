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
    public Material[] eyes; //eye array
    private Material[] loadout = new Material[3]; //loadout of custom items
    private SkinnedMeshRenderer MeshSkin; //element grabbing skinned mesh renderer
    private float red, blue, green; //floats for skin color
    private Animator anim;
    public Slider sred, sblue, sgreen; //sliders for skin color
    public int skincount = 0;
    public int shirtcount = -1;
    public int eyecount = -1;
    public float timeLeft;
    string prefabPath = "Assets/Prefabs/Player01.prefab";

    public void SetButtonIdShirt(int id) //on button press sets ID
    {
        shirtcount = id;
        Debug.Log(shirtcount);
        anim.SetBool("PlayCheckOut", false);
        anim.SetBool("PlayCheckOut", true);
        timeLeft = 1f;
        loadout[1] = shirts[shirtcount];
        MeshSkin.materials = loadout;
    }

    public void SetButtonIdEyes(int id) //on button press sets ID
    {
        eyecount = id;
        Debug.Log(eyecount);
        loadout[2] = eyes[eyecount];
        MeshSkin.materials = loadout;
    }

    void Start()
    {
        anim = transform.parent.gameObject.GetComponent<Animator> ();
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
        MeshSkin.materials = loadout;
        /* old way of itteratting one at a time
       ///////////////////////////////////////Shirts/////////////////////////////////////////////////////////////////////////////////////////////////
       if (Input.GetKeyDown("d")) //increment shirt count and save to loadout
       {
           shirtcount++;
           anim.SetBool("PlayCheckOut", false);
           anim.SetBool("PlayCheckOut", true);
           timeLeft = 1f;
           if (shirtcount >= shirts.Length)
           {
               shirtcount = 0;
           }
           loadout[1] = shirts[shirtcount];
           MeshSkin.materials = loadout;

           //anim.SetBool("PlayCheckout", false);
       }

       if (Input.GetKeyDown("a")) //increment shirt count and save to loadout
       {
           shirtcount--;
           anim.SetBool("PlayCheckOut", false);
           anim.SetBool("PlayCheckOut", true);
           timeLeft = 1f;
           if (shirtcount < 0)
           {
               shirtcount = shirts.Length - 1;
           }
           loadout[1] = shirts[shirtcount];
           MeshSkin.materials = loadout;
       }


       if (Input.GetKeyDown("w")) //increment shirt count and save to loadout
       {
           eyecount++;
           if (eyecount >= eyes.Length)
           {
               eyecount = 0;
           }
           loadout[2] = eyes[eyecount];
           MeshSkin.materials = loadout;

           //anim.SetBool("PlayCheckout", false);
       }

       if (Input.GetKeyDown("s")) //increment shirt count and save to loadout
       {
           eyecount--;
           timeLeft = 1f;
           if (eyecount < 0)
           {
               eyecount = eyes.Length - 1;
           }
           loadout[2] =eyes[eyecount];
           MeshSkin.materials = loadout;
       }
       //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       */


        if (timeLeft > -1)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                anim.SetBool("PlayCheckOut", false);
            }
        }

    }
}
