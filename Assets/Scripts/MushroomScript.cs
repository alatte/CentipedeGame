using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject[] bonuses;

    private int lifes = 4;

    void Start()
    {
        UpdateSpriteForMushroom();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shot")
        {
            Destroy(other.gameObject);
            if (--lifes == 0)
            {
                SpawnBonus();
                Destroy(gameObject);
            }
            else
                UpdateSpriteForMushroom();
        }
    }

    void UpdateSpriteForMushroom()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[lifes-1];
    }

    private void SpawnBonus()
    {
        if(Random.Range(0, 10) == 1)
        {
            Instantiate(bonuses[Random.Range(0, bonuses.Length)], transform.position, Quaternion.identity);
        }
    }
}
