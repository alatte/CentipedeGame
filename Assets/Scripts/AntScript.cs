using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntScript : MonoBehaviour
{
    public GameObject mushroom;

    private float speed;
    private float delay;
    private float nextTime;

    void Start()
    {
        SetRandomSpeed();
        SetRandomDelay();
    }

    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + delay;
            SpawnMushroom();
        }
        AntMove();
    }

    void AntMove()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void SpawnMushroom()
    {
        Instantiate(mushroom, transform.position, Quaternion.identity);
    }

    void SetRandomSpeed()
    {
        speed = Random.Range(4f, 10f);
    }

    void SetRandomDelay()
    {
        delay = Random.Range(0.2f, 0.5f);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
            Destroy(gameObject);
    }
}
