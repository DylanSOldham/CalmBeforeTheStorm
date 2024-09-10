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

    Vector3 eh_position;
    Vector3 eh_point1;
    Vector3 eh_point2;
    public Vector3 GetNormalAtPosition(Vector3 position)
    {

        Vector3 point1 = position + 0.1f * Vector3.forward;
        point1.y = GetHeightAtPosition(point1);

        Vector3 point2 = position + 0.1f * Vector3.right;
        point2.y = GetHeightAtPosition(point2);

        Vector3 tangent1 = point1 - position;
        Vector3 tangent2 = point2 - position;

        eh_position = position;
        eh_point1 = tangent1;
        eh_point2 = tangent2;

        return Vector3.Cross(tangent1, tangent2).normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(eh_position, eh_position + 10.0f * GetNormalAtPosition(eh_position));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(eh_position, eh_position + 150.0f * eh_point1);
        Gizmos.DrawLine(eh_position, eh_position + 150.0f * eh_point2);
    }
}
