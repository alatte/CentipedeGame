using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawnScript : MonoBehaviour
{
    public int numOfMushrooms = 30;
    public GameObject mushroom;

    private float mushroomRadius = 0.5f;
    private float height;
    private float width;

    void Start()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        for (int i = 0; i < numOfMushrooms; i++)
        {
            Instantiate(mushroom, GetRandomPosition(), Quaternion.identity);
        }
    }

    Vector2 GetRandomPosition()
    {
        return new Vector2((int)Random.Range(-width / 2 + mushroomRadius, width / 2 - mushroomRadius), (int)Random.Range(-height / 2 + mushroomRadius + 1, height / 2 - mushroomRadius));

    }
}
