using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeScript : MonoBehaviour
{
    private GameControllerScript gameControllerScript;

    private void Start()
    {
        gameControllerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    void Update()
    {
        if (transform.childCount == 0)
        {    
            gameControllerScript.CentipedeLose();
            Destroy(gameObject);
        }
    }

    public void CollisionWithWallDetected(CentipedeMovingScript centipedeMovingScript)
    {
        gameControllerScript.CentipedeWin();
        Destroy(gameObject);
    }
}
