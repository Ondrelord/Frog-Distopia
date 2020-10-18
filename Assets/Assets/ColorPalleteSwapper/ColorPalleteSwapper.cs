using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalleteSwapper : MonoBehaviour
{
    [SerializeField] Material material = null;

    [SerializeField] Color[] originalPallete = { };
    [SerializeField] Color[] swapPallete = { };
    void OnValidate()
    {
        // create texture to swap
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        // filling with empty pixels
        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        // setting pallete into texture
        for (int j = 0; j < originalPallete.Length; ++j)
            colorSwapTex.SetPixel( Mathf.FloorToInt(originalPallete[j].r*256), 0, swapPallete[j]);

        colorSwapTex.Apply();

        // creating instatnce of material (due to errors in edit mode)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Material tempMaterial = new Material(material);
        sr.sharedMaterial = tempMaterial;
        sr.sharedMaterial.SetTexture("_SwapTex", colorSwapTex);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
