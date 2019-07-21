using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawnScript : MonoBehaviour
{
    public GameObject ant;

    [SerializeField]
    private float delay = 15f;
    private float nextTime;
    private float height;
    private float width;

    private void Start()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void Update()
    {
        if(Time.time > nextTime)
        {
            nextTime = Time.time + delay;
            SpawnAnt();
        }
    }

    void SpawnAnt()
    {
        Instantiate(ant, GetAntPosition(), Quaternion.identity);
    }

    Vector2 GetAntPosition()
    {
        return new Vector2(Random.Range(-width / 2 + 1, width / 2 - 1), height / 2 - 1);
    }
}
