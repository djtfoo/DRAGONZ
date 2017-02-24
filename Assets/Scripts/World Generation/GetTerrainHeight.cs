using UnityEngine;
using System.Collections;

public static class GetTerrainHeight {

    public static float GetHeight(GameObject terrain, Vector3 pos)
    {
        // get mesh
        //Mesh mesh = terrain.GetComponent<MeshFilter>().mesh;

        float scale = terrain.transform.localScale.x;
        //Debug.Log(scale);

        int mapChunkSize = MapData.mapChunkSize - 1;
        int halfMapChunkSize = MapData.mapChunkSize / 2;
        // get position in the mesh
        int x = (int)(pos.x / scale) + halfMapChunkSize;
        int z = (int)(pos.z / scale) + halfMapChunkSize;

        //return mesh.vertices[z * mapChunkSize + x].y;

        return Noise.noiseMap[x, mapChunkSize - z];
    }

    public static float GetHeight(GameObject terrain, float x, float z)
    {
        // get mesh
        //Mesh mesh = terrain.GetComponent<MeshFilter>().mesh;

        float scale = terrain.transform.localScale.x;
        //Debug.Log(scale);

        int mapChunkSize = MapData.mapChunkSize - 1;
        int halfMapChunkSize = MapData.mapChunkSize / 2;
        // get position in the mesh
        int valX = (int)(x / scale) + halfMapChunkSize;
        int valZ = (int)(z / scale) + halfMapChunkSize;

        //return mesh.vertices[z * mapChunkSize + x].y;

        return Noise.noiseMap[valX, mapChunkSize - valZ];

    }

}
