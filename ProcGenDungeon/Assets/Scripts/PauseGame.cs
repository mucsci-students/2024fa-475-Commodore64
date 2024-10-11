using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenuUI.SetActive(false);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
        SceneManager.LoadScene("StartingRoom");
    }

    public void Quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
