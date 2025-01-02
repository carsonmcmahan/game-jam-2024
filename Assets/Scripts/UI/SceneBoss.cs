using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject creditsInfo;
    private bool active;
    void Start()
    {
        creditsInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        SceneManager.LoadScene("GameScene");
    }

    public void Credits() {
        Debug.Log("Hoe");
        creditsInfo.SetActive(!active);
        active = !active;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}