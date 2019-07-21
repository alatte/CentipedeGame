using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    public GameObject startButton;
    public GameObject endlessButton;

    void Start()
    {
        startButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
            PlayerPrefs.SetInt("endlessMode", 0);
        });
        endlessButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
            PlayerPrefs.SetInt("endlessMode", 1);
        });
    }
}
