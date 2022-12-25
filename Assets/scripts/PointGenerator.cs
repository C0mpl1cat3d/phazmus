using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    public List<GameObject> centerPoints;
    public GameObject pointPrefab;

    public int iterations;
    public int rows;
    public float xOffset;
    public float yOffset;
    public float oddRowXOffset;

    void Start()
    {
        centerPoints = new List<GameObject>();
        Vector2 currentPos = transform.position;
        for (int i = 0; i < rows * iterations; i++)
        {
            GameObject point = Instantiate(pointPrefab, currentPos, Quaternion.identity);
            centerPoints.Add(point);
            currentPos.x += xOffset;
            if ((i + 1) % iterations == 0)
            {
                if ((i / iterations) % 2 == 0)
                {
                    currentPos.x = transform.position.x + oddRowXOffset;
                }
                else
                {
                    currentPos.x = transform.position.x;
                }

                currentPos.y -= yOffset;
            }
        }
    }
}