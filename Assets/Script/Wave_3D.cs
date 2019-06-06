using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_3D : MonoBehaviour
{
    public Manger manager;
    private Vector3[] originalVertices;
    private Mesh mesh;
    private float density = 1.0f;
    private float v_p = (float)(4500.0f / 1000.0f);
    private float v_s = (float)(2500.0f / 1000.0f);
    private float d = 0.0f;
    private float f = 10.0f;
    private float lambda = 1.0f;
    private float A = 1.0f;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        density = manager.DensityVelocityValue().density;
        v_p = manager.DensityVelocityValue().v_p / 1000.0f;
        v_s = manager.DensityVelocityValue().v_s / 1000.0f;

        A = (float)(Mathf.Pow(10, (float)(manager.Magnitude(Target.T_3D) - 2.56 * (Mathf.Log10(4)) + 1.67)) / 1000);

    }

    void Update()
    {
        density = manager.DensityVelocityValue().density;
        v_p = manager.DensityVelocityValue().v_p / 1000.0f;
        v_s = manager.DensityVelocityValue().v_s / 1000.0f;

        A = (float)(Mathf.Pow(10, (float)(manager.Magnitude(Target.T_3D) - 2.56 * (Mathf.Log10(4)) + 1.67)) / 1000);

        Vector3[] newVertices = new Vector3[originalVertices.Length];
        if (manager.Apply3D())
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
            d = v_p * (timeCode);
            float r = Mathf.Sqrt(Mathf.Pow(origin.x, 2) + Mathf.Pow(origin.y, 2));
            float cos_theta = origin.x / r;
            float sin_theta = origin.y / r;
            Vector3 newVertex = origin;
            if (r < d)
            {
                //Debug.Log("lalala");
                newVertex = new Vector3(
                origin.x + (cos_theta) * (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (r / v_p)))),
                origin.y + (sin_theta) * (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (r / v_p)))),
                origin.z);
            };

                return newVertex;
        }
        else if (manager.ApplyS())
        {
            d = v_s * (timeCode);
            float r = Mathf.Sqrt(Mathf.Pow(origin.x, 2) + Mathf.Pow(origin.y, 2));
            Vector3 newVertex = origin;

            if (r < d)
            {
                newVertex = new Vector3(
                 origin.x,
                 origin.y,
                 origin.z + (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (r / v_s)))));
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