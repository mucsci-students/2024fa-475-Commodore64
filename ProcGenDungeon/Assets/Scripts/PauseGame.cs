using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject[] player;

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
        SceneManager.LoadScene(4);
        player = GameObject.FindGameObjectsWithTag("Player");
        player[0].transform.position = new Vector3(0f, 0f, 0f);

        player[0].GetComponent<Player>().currentHealth = 100;
        player[0].GetComponent<Player>().currentEnergy = 100;
        player[0].GetComponent<Player>().isDead = false;
        player[0].GetComponent<Animator>().enabled = true;
        player[0].GetComponent<Animator>().SetBool("dead", false);
        player[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/Player/RPG_Hero/idle/idle_down_40x40_2.png");
        player[0].GetComponent<Animator>().Play("IdleAnim");
        player[0].GetComponent<Player>().inventory = new Inventory(18);
        player[0].GetComponent<Player>().healthBar.SetHealth(100);
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
