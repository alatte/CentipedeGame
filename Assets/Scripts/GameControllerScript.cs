using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public GameObject lifesText;
    public GameObject player;
    [SerializeField]
    private int maxCentipedeBodySize = 7;
    [SerializeField]
    private float startCentipedeSpeed = 2;
    private float currentCentipedeSpeed;
    private int currentBodySize;
    private int endlessMode;

    private void Start()
    {
        endlessMode = PlayerPrefs.GetInt("endlessMode");
        currentBodySize = maxCentipedeBodySize;
        currentCentipedeSpeed = startCentipedeSpeed;
        GetComponent<CentipedeSpawnScript>().SpawnCentipede(currentBodySize--, currentCentipedeSpeed++);
    }

    public void WriteLifes(int lifes)
    {
        if (lifes != 0)
            lifesText.GetComponent<UnityEngine.UI.Text>().text = "Lifes: " + lifes;
        else SceneManager.LoadScene("LoseScene");
    }

    public void CentipedeWin()
    {
        GetComponent<CentipedeSpawnScript>().SpawnCentipede(currentBodySize, currentCentipedeSpeed);
        player.GetComponent<PlayerScript>().LoseLife();
    }

    public void CentipedeLose()
    {
        int numOfCentipedies = GameObject.FindGameObjectsWithTag("Centipede").Length;
        if (GameObject.FindGameObjectsWithTag("Centipede").Length == 1)
        {
            if (currentBodySize != 0)
            {
                GetComponent<CentipedeSpawnScript>().SpawnCentipede(currentBodySize--, currentCentipedeSpeed++);
                GetComponent<CentipedeSpawnScript>().SpawnCentipodeHead();
            }
            else if (currentBodySize == 0 && endlessMode == 1)
            {
                currentBodySize = maxCentipedeBodySize;
                currentCentipedeSpeed = startCentipedeSpeed;
                GetComponent<CentipedeSpawnScript>().SpawnCentipede(currentBodySize--, currentCentipedeSpeed++);
            }
            else if (currentBodySize == 0 && endlessMode == 0)
                SceneManager.LoadScene("WinScene");
        }
    }
}
