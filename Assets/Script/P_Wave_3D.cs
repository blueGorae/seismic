using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Wave_3D : MonoBehaviour
{
    public Manger manger;
    private Vector3[] originalVertices;
    private Mesh mesh;
    private Vector3 startPoint = new Vector3(0.0f, 0.0f, 0.0f);
    private float v = 6.0f;
    private float A = 1.0f;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }


    void Update()
    {

        Vector3[] newVertices = new Vector3[originalVertices.Length];

        for (int i = 0; i < originalVertices.Length; i++)
        {
            newVertices[i] = WaveFunction(originalVertices[i], Time.time);
        }

        mesh.vertices = newVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    private Vector3 WaveFunction(Vector3 origin, float timeCode)
    {
        // sine wave with an amplitude of 1 unit and a period of 2π units, 
        // traveling with a speed of 1 unit per second.
        // Change this to your own wave function.

        float d = Mathf.Sqrt(origin.x * origin.x + origin.y * origin.y);
        float cos_theta = origin.x / d;
        float sin_theta = origin.y / d;

        return new Vector3(
            origin.x + (cos_theta) * (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (d / v)))),
            origin.y + (sin_theta) * (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (d / v)))),
             origin.z 
        );

    }
}