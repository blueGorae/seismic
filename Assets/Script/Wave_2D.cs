using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_2D : MonoBehaviour
{
    public Manger manager;
    private Vector3[] originalVertices;
    private Mesh mesh;
    private float density = 1.0f;
    private float scale = 1000f;
    private float v_p;
    private float v_s;
    private float d = 0.0f;
    private float f = 10.0f;
    private float lambda = 1.0f;
    private float A = 1.0f;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        density = manager.DensityVelocityValue().density;
        v_p = manager.DensityVelocityValue().v_p / scale;
        v_s = manager.DensityVelocityValue().v_s / scale;

        A = (float)(Mathf.Pow(10, (float)(manager.Magnitude(Target.T_2D) - 2.56 * (Mathf.Log10(4)) + 1.67)) / scale);

    }

    void Update()
    {
        density = manager.DensityVelocityValue().density;
        float adjustScale = 1000f;
        v_p = manager.DensityVelocityValue().v_p / adjustScale;
        v_s = manager.DensityVelocityValue().v_s / adjustScale;

        A = (float)(Mathf.Pow(10, (float)(manager.Magnitude(Target.T_2D) - 2.56 * (Mathf.Log10(4)) + 1.67)) / adjustScale);

        Vector3[] newVertices = new Vector3[originalVertices.Length];
        if (manager.Apply2D())
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
        if (manager.ApplyP())
        {
            Vector3 newVertex = origin;

            d = v_p * (timeCode % 5);
            lambda = (float)(v_p / f);
            if (origin.x < d)
            {
                newVertex = new Vector3((float)(origin.x - A * Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (origin.x / v_p)))),
                origin.y,
                origin.z);
            }

            return newVertex;
        }
        else if (manager.ApplyS())
        {
            Vector3 newVertex = origin;
            d = v_s * (timeCode % 5);
            lambda = (float)(v_s / f);
            if (origin.x < d)
            {
                newVertex = new Vector3(
                origin.x,
                origin.y,
                origin.z + (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (origin.x / v_s)))));

            }

            return newVertex;
        }
        else if (manager.ApplyEarthquake())
        {
            return origin;
        }
        else
        {
            return origin;
        }
    }
}