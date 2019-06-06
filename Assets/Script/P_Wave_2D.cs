using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Wave_2D : MonoBehaviour
{
    public Manger manger;
    private Vector3[] originalVertices;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }


    void Update()
    {
        Vector3[] newVertices = new Vector3[originalVertices.Length];
        if (manger.ApplyP2D())
        {
            for (int i = 0; i < originalVertices.Length; i++)
            {
                newVertices[i] = WaveFunction(originalVertices[i], Time.time);
            }
        }
        else
        {
            newVertices = originalVertices;
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
        return new Vector3(
             (float)(origin.x - Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (origin.x / 6)))),
             origin.y,
             origin.z
        );
    }
}