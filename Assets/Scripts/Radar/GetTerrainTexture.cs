using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class GetTerrainTexture {

    static Texture2D texture;

    public static void SetTexture2D(Texture2D tex)
    {
        GetTerrainTexture.texture = tex;
        //for (int i = 0; i < 240; ++i)
        //{
        //    for (int j = 0; j < 240; ++j)
        //    {
        //        texture.SetPixel(i, j, tex.GetPixel(i, j));
        //    }
        //}
        //
        //texture.Apply();
    }

    public static Texture GetTexture2D()
    {
        return texture;
    }

}
