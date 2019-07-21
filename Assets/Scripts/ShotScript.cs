using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    void Update()
    {
        MoveShot();
    }

    //Just moves shot up
    void MoveShot()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //Destroys shot after it's outside the camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
