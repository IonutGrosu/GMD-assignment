using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;
    
    Vector3[] vertices;
    private int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i=0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int currentVertex = 0;
        int trianglesCount = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[trianglesCount + 0] = currentVertex + 0;
                triangles[trianglesCount + 1] = currentVertex + xSize + 1;
                triangles[trianglesCount + 2] = currentVertex + 1;
                triangles[trianglesCount + 3] = currentVertex + 1;
                triangles[trianglesCount + 4] = currentVertex + xSize + 1;
                triangles[trianglesCount + 5] = currentVertex + xSize + 2;

                currentVertex++;
                trianglesCount += 6;
            }

            currentVertex++;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
