using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubes : MonoBehaviour
{
    const int WIDTH = 11;
    const int HEIGHT = 11;
    const int DEPTH = 11;
    const float WIDTH_2 = WIDTH * .5f;
    const float HEIGHT_2 = HEIGHT * .5f;
    const float DEPTH_2 = DEPTH * .5f;
    private static readonly GameObject[,,] cubes = new GameObject[DEPTH, HEIGHT, WIDTH];

    public static GameObject[,,] Cubes => cubes;
    // Start is called before the first frame update
    void Start()
    {

        for (int d = 0; d < DEPTH; d++)
        {
            for (int h = 0; h < HEIGHT; h++)
            {
                for (int w = 0; w < WIDTH; w++)
                {

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Vector3 originPosition = new Vector3(w - WIDTH_2, h - HEIGHT_2, d - DEPTH_2);
                    cube.transform.position = originPosition;

                    cube.AddComponent<LineRenderer>();
                    LineRenderer lr = cube.GetComponent<LineRenderer>();
                    lr.positionCount = 4;
                    float lineWidth = 0.1f;
                    lr.widthMultiplier = lineWidth;

                    lr.startColor = Color.black;
                    lr.endColor = Color.black;


                    Cubes[d, h, w] = cube;

                }
            }
        }

    }


    // Update is called once per frame
    void Update()
    {

        for (int d = 0; d < DEPTH; d++)
        {
            for (int h = 0; h < HEIGHT; h++)
            {
                for (int w = 0; w < WIDTH; w++)
                {
                    Cubes[d, h, w].transform.position += new Vector3((float)(-0.01 * Mathf.Cos(2 * Mathf.PI - (Time.fixedTime - ((float)w / 6)))), 0, 0);

                    LineRenderer lr = Cubes[d, h, w].GetComponent<LineRenderer>();
                    float lineWidth = 0.1f;

                    lr.SetPosition(0, Cubes[d, h, w].transform.position - new Vector3(0, -(0.5f + lineWidth / 2.0f), -(0.5f + lineWidth / 2.0f)));
                    lr.SetPosition(1, Cubes[d, h, w].transform.position - new Vector3(0, (0.5f + lineWidth / 2.0f), -(0.5f + lineWidth / 2.0f)));
                    lr.SetPosition(2, Cubes[d, h, w].transform.position - new Vector3(0, (0.5f + lineWidth / 2.0f), (0.5f + lineWidth / 2.0f)));
                    lr.SetPosition(3, Cubes[d, h, w].transform.position - new Vector3(0, -(0.5f + lineWidth / 2.0f), (0.5f + lineWidth / 2.0f)));
                }
            }
        }

    }
}