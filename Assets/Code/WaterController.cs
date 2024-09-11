using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public const float WATER_SIZE = 1500.0f;
    public const int WATER_VERTEX_WIDTH = 100;
    const float SQUARE_SIZE = WATER_SIZE / WATER_VERTEX_WIDTH;

    public StormController stormController;
    private float waveAmplitude = 20.0f;
    private float targetWaveAmplitude = 20.0f;
    
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        vertices = new Vector3[WATER_VERTEX_WIDTH * WATER_VERTEX_WIDTH];

        List<int> triangles_list = new List<int>();
        for (int i = 0; i < WATER_VERTEX_WIDTH - 1; ++i)
        {
            for (int j = 0; j < WATER_VERTEX_WIDTH - 1; ++j)
            {
                triangles_list.Add(j * WATER_VERTEX_WIDTH + i);
                triangles_list.Add((j + 1) * WATER_VERTEX_WIDTH + (i + 1));
                triangles_list.Add(j * WATER_VERTEX_WIDTH + (i + 1));

                triangles_list.Add(j * WATER_VERTEX_WIDTH + i);
                triangles_list.Add((j + 1) * WATER_VERTEX_WIDTH + i);
                triangles_list.Add((j + 1) * WATER_VERTEX_WIDTH + (i + 1));
            }
        }
        triangles = triangles_list.ToArray();

        RefreshVertices();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RefreshVertices();

        if (stormController.IsStormActive())
        {
            targetWaveAmplitude = 30.0f;
        }
        else {
            targetWaveAmplitude = 10.0f;
        }
        waveAmplitude = Mathf.Lerp(waveAmplitude, targetWaveAmplitude, 0.005f);
        Debug.Log(waveAmplitude);
    }

    void RefreshVertices()
    {
        timer += 0.001f;

        if (Time.deltaTime > 0.02)
            Debug.Log(Time.deltaTime);

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
                vertices[j * WATER_VERTEX_WIDTH + i].y = GetHeightAtPosition(transform.position + vertices[j * WATER_VERTEX_WIDTH + i]);
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    public float GetHeightAtPosition(Vector3 position) 
    {
        return waveAmplitude * Mathf.PerlinNoise(position.x * 0.01f + timer, position.z * 0.01f + timer)
             + waveAmplitude * Mathf.PerlinNoise(-position.x * 0.01f + timer, -position.z * 0.01f + timer);
    }

    public Vector3 GetNormalAtPosition(Vector3 position)
    {
        Vector3 point1 = position + 0.4f * Vector3.forward;
        point1.y = GetHeightAtPosition(point1);

        Vector3 point2 = position + 0.4f * Vector3.right;
        point2.y = GetHeightAtPosition(point2);

        Vector3 tangent1 = point1 - position;
        Vector3 tangent2 = point2 - position;

        return Vector3.Cross(tangent1, tangent2).normalized;
    }
}
