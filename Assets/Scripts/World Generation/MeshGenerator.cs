using UnityEngine;
using System.Collections;

public static class MeshGenerator {

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement = levelOfDetail * 2;
        if (meshSimplificationIncrement == 0)
            meshSimplificationIncrement = 1;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIdx = 0;

        for (int z = 0; z < height; z += meshSimplificationIncrement) {
            for (int x = 0; x < width; x += meshSimplificationIncrement) {

                meshData.vertices[vertexIdx] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, z]) * heightMultiplier, topLeftZ - z);
                meshData.UVs[vertexIdx] = new Vector2(x / (float)width, z / (float)height);

                if (x < width - 1 && z < height - 1) {
                    meshData.AddTriangle(vertexIdx, vertexIdx + verticesPerLine + 1, vertexIdx + verticesPerLine);
                    meshData.AddTriangle(vertexIdx + verticesPerLine + 1, vertexIdx, vertexIdx + 1);
                }

                ++vertexIdx;
            }
        }

        return meshData;
    }

}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] UVs;

    int triIdx;     // triangle index

    public MeshData(int meshWidth, int meshHeight) {
        vertices = new Vector3[meshWidth * meshHeight];
        UVs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];

        triIdx = 0;
    }

    public void AddTriangle(int v1, int v2, int v3) {
        triangles[triIdx] = v1;
        triangles[triIdx + 1] = v2;
        triangles[triIdx + 2] = v3;
        triIdx += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = UVs;
        mesh.RecalculateNormals();

        return mesh;
    }

}