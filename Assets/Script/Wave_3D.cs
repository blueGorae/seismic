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
    private float scale = 0.1f;
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

        A = (float)(Mathf.Pow(10, (float)(manager.Magnitude(Target.T_3D) - 2.56 * (Mathf.Log10(4)) + 1.67)) / scale);

    }

    void Update()
    {
        density = manager.DensityVelocityValue().density;
        float adjustScale = manager.ApplyGrassland() ? 1f : 1000f;
        v_p = manager.DensityVelocityValue().v_p / adjustScale;
        v_s = manager.DensityVelocityValue().v_s / adjustScale;

        A = (float)(Mathf.Pow(10, (float)(manager.Magnitude(Target.T_3D) - 2.56 * (Mathf.Log10(4)) + 1.67)) / adjustScale);

        Vector3[] newVertices = new Vector3[originalVertices.Length];
        if (manager.Apply3D() || manager.ApplyGrassland())
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
            float r = Mathf.Sqrt(Mathf.Pow(origin.x, 2) + Mathf.Pow(origin.y, 2));

            float d_p_start = v_p * timeCode;
            float d_p_end = d_p_start - v_p * 2;
            float cos_theta = origin.x / r;
            float sin_theta = origin.y / r;

            float d_s_start = v_s * timeCode;
            float d_s_end = d_s_start - v_s * 2;
           

            Vector3 newVertex = origin;

            if(r > d_p_end && r< d_p_start)
            {
                newVertex = new Vector3(
                   origin.x + (cos_theta) * (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (r / v_p)))),
                   origin.y + (sin_theta) * (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (r / v_p)))),
                   origin.z);

            }

            if(r > d_s_end && r < d_s_start)
            {
                newVertex = new Vector3(
                 origin.x,
                 origin.y,
                 origin.z + (float)A * (Mathf.Cos(2 * Mathf.PI * (Time.fixedTime - (r / v_s)))));
            }

            return newVertex;
        }
        else
        {
            return origin;
        }
    }
}