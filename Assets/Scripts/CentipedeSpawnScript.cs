using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawnScript : MonoBehaviour
{
    public GameObject centipede;
    public GameObject partOfCentipede;

    [SerializeField]
    private float startSpeedCentepede;
    private float height;
    private float width;
    private int centipedeBodySize;

    private List<GameObject> listOfPartsCentipede;

    void Start()
    {
        listOfPartsCentipede = new List<GameObject>();
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    public void SpawnCentipede(int centipedeBodySize, float speed)
    {
        this.centipedeBodySize = centipedeBodySize;
        this.startSpeedCentepede = speed;
        //Create empty centipede
        GameObject centipedeParent = Instantiate(centipede, GetCentipedePosition(), Quaternion.identity);
        CreateCentipedeBody(centipedeParent);
    }

    //Creates some parts of body inside centipede
    void CreateCentipedeBody(GameObject centipedeParent)
    {
        for (int i = 0; i < centipedeBodySize; i++)
        {
            GameObject newPart = Instantiate(partOfCentipede, centipedeParent.transform.position, Quaternion.identity);
            newPart.GetComponent<CentipedeMovingScript>().SetCentipedeSpeed(startSpeedCentepede);
            newPart.transform.SetParent(centipedeParent.transform);

            //And set target for each non-head parts
            if (listOfPartsCentipede.Count > 0)
                newPart.GetComponent<CentipedeMovingScript>().SetTarget(listOfPartsCentipede[listOfPartsCentipede.Count - 1]);
            listOfPartsCentipede.Add(newPart);
        }

        listOfPartsCentipede.Clear();
    }

    public void SpawnCentipodeHead()
    {
        GameObject centipedeParent = Instantiate(centipede, GetCentipedePosition(), Quaternion.identity);
        GameObject newPart = Instantiate(partOfCentipede, centipedeParent.transform.position, Quaternion.identity);
        newPart.GetComponent<CentipedeMovingScript>().SetCentipedeSpeed(startSpeedCentepede * 1.5f);
        newPart.transform.SetParent(centipedeParent.transform);
    }

    //Get random position for centipede spawn
    Vector2 GetCentipedePosition()
    {
        return new Vector2(Random.Range(-width / 2 + 1, width / 2 - 1), height / 2 - 1);
    }
}
