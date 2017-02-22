﻿using UnityEngine;
using System.Collections;

public static class GetTerrainHeight {

    public static float GetHeight(GameObject terrain, Vector3 pos)
    {
        // get mesh
        //Mesh mesh = terrain.GetComponent<MeshFilter>().mesh;

        float scale = terrain.transform.localScale.x;
        Debug.Log(scale);

        const int mapChunkSize = 240;
        const int halfMapChunkSize = 120;
        // get position in the mesh
        int x = (int)(pos.x / scale) + halfMapChunkSize;
        int z = (int)(pos.z / scale) + halfMapChunkSize;

        //return mesh.vertices[z * mapChunkSize + x].y;

        return Noise.noiseMap[x, mapChunkSize - z];

    }

}