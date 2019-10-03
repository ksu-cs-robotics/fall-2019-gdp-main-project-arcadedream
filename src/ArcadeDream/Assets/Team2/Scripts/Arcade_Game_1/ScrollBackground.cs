using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// scrolls the stars across the background
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>

public class ScrollBackground : MonoBehaviour
{
    private Material material_m;
    private Vector2 offset_m;
    public float x, y;

    private void Awake()
    {
        material_m = GetComponent<Renderer>().material;
        material_m.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    private void Start()
    {
        offset_m = new Vector2(x, y);
    }

    private void Update()
    {
        material_m.mainTextureOffset += offset_m * Time.deltaTime;
    }
}
