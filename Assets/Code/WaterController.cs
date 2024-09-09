using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public const float WATER_SIZE = 200.0f;
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
    void Update()
    {
        RefreshVertices();
    }

    void RefreshVertices()
    {
        timer += 0.1f * Time.deltaTime;

        mesh.Clear();
        for (int i = 0; i < WATER_VERTEX_WIDTH; i++)
        {
            for (int j = 0; j < WATER_VERTEX_WIDTH; j++)
            {
                vertices[j * WATER_VERTEX_WIDTH + i] = new Vector3(
                    (float)i * SQUARE_SIZE - WATER_SIZE / 2.0f, 
                    20.0f * Mathf.PerlinNoise((float)i * 0.02f + timer, (float) j * 0.02f + timer), 
                    (float)j * SQUARE_SIZE - WATER_SIZE / 2.0f
                );
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
    public float GetHeightAtPosition(Vector3 position) 
    {
        int index_x = Mathf.FloorToInt((position.x + WATER_SIZE / 2.0f) / SQUARE_SIZE);
        int index_z = Mathf.FloorToInt((position.z + WATER_SIZE / 2.0f) / SQUARE_SIZE);

        if (index_x < 0 || index_x >= WATER_VERTEX_WIDTH - 1 || index_z < 0 || index_z >= WATER_VERTEX_WIDTH - 1) {
            return float.NaN;
        }

        Vector3 vertexTopleft = vertices[index_z * WATER_VERTEX_WIDTH + index_x];
        Vector3 vertexBottomleft = vertices[(index_z + 1) * WATER_VERTEX_WIDTH + index_x];
        Vector3 vertexTopright = vertices[index_z * WATER_VERTEX_WIDTH + (index_x + 1)];
        Vector3 vertexBottomright = vertices[(index_z + 1) * WATER_VERTEX_WIDTH + (index_x + 1)];

        float xDist1 = (position.x - vertexTopleft.x) / SQUARE_SIZE;
        float xInterpolated1 = Mathf.Lerp(vertexTopleft.y, vertexTopright.y, xDist1);

        float xDist2 = (position.x - vertexBottomleft.x) / SQUARE_SIZE;
        float xInterpolated2 = Mathf.Lerp(vertexBottomleft.y, vertexBottomright.y, xDist2);

        float interpolatedY = Mathf.Lerp(xInterpolated1, xInterpolated2, (position.z - vertexTopleft.z) / SQUARE_SIZE);

        return interpolatedY;
    }
}
