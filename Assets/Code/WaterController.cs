using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public const float WATER_SIZE = 500.0f;
    public const int WATER_VERTEX_WIDTH = 100;
    const float SQUARE_SIZE = WATER_SIZE / WATER_VERTEX_WIDTH;

    private Mesh mesh;

    private Vector3[] vertices;
    private List<int> triangles;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new Vector3[WATER_VERTEX_WIDTH * WATER_VERTEX_WIDTH];

        triangles = new List<int>();
        for (int i = 0; i < WATER_VERTEX_WIDTH - 1; ++i)
        {
            for (int j = 0; j < WATER_VERTEX_WIDTH - 1; ++j)
            {
                triangles.Add(j * WATER_VERTEX_WIDTH + i);
                triangles.Add((j + 1) * WATER_VERTEX_WIDTH + (i + 1));
                triangles.Add(j * WATER_VERTEX_WIDTH + (i + 1));

                triangles.Add(j * WATER_VERTEX_WIDTH + i);
                triangles.Add((j + 1) * WATER_VERTEX_WIDTH + i);
                triangles.Add((j + 1) * WATER_VERTEX_WIDTH + (i + 1));
            }
        }

        RefreshVertices();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RefreshVertices();
    }

    void RefreshVertices()
    {
        timer += 0.001f;

        mesh.Clear();
        for (int i = 0; i < WATER_VERTEX_WIDTH; i++)
        {
            for (int j = 0; j < WATER_VERTEX_WIDTH; j++)
            {
                vertices[j * WATER_VERTEX_WIDTH + i] = new Vector3(
                    (float)i * SQUARE_SIZE - WATER_SIZE / 2.0f, 
                    0.0f, 
                    (float)j * SQUARE_SIZE - WATER_SIZE / 2.0f
                );
                vertices[j * WATER_VERTEX_WIDTH + i].y = GetHeightAtPosition(vertices[j * WATER_VERTEX_WIDTH + i]);
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
    public float GetHeightAtPosition(Vector3 position) 
    {
        return 20.0f * Mathf.PerlinNoise(position.x * 0.02f + timer, position.z * 0.02f + timer);
    }
}
