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
        // if not paused
        if (Time.timeScale == 1)
        {
            // pause and activate pause menu
            Time.timeScale = 0;
            PauseMenuUI.SetActive(true);
        }
        else
        {
            // if paused, resume game and remove pause menu
            Time.timeScale = 1;
            PauseMenuUI.SetActive(false);
        }
    }

    public void Restart()
    {
        // Reset every player and game element to base values
        // including time, position, score, health and energy, inventory, etc.
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
        SceneManager.LoadScene("Restart Room");
        player = GameObject.FindGameObjectsWithTag("Player");
        player[0].transform.position = new Vector3(0f, 0f, 0f);

        player[0].GetComponent<Player>().score = 0;
        player[0].GetComponent<Player>().currentHealth = 100;
        player[0].GetComponent<Player>().currentEnergy = 100;
        player[0].GetComponent<Player>().isDead = false;
        player[0].GetComponent<Animator>().enabled = true;
        player[0].GetComponent<Animator>().SetBool("dead", false);
        player[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/Player/RPG_Hero/idle/idle_down_40x40_2.png");
        player[0].GetComponent<Animator>().Play("IdleAnim");
        for (int i = 0; i < 21; i++)
        {
            for (int j = 0; j < player[0].GetComponent<Player>().inventory.slots[i].maxAllowed; j++)
                player[0].GetComponent<Player>().inventory.Remove(i);
        }
        player[0].GetComponent<Player>().healthBar.SetHealth(100);
        player[0].GetComponent<Player>().energyBar.SetEnergy(100);
        player[0].GetComponent<Player>().damage = 20;
        player[0].GetComponent<Player>().armor = 10;
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