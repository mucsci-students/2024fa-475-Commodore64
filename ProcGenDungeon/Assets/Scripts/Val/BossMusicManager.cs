using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMusicManager : MonoBehaviour
{
    public GameObject player;
    public GameObject bossTracker;
    public GameObject bossSong;
    public GameObject bossDeadSound;
    public GameObject victoryMusic;
    public BossTracker bts;
    public Text scoreText;
    public bool started;

    void Start()
    {
        player = GameObject.Find("Hero");
        bossTracker = GameObject.Find("BossTracker");
        bossDeadSound = GameObject.Find("BossKilledSound");
        bossSong = GameObject.Find("BossMusic");
        victoryMusic = GameObject.Find("VicSong");
        bts = bossTracker.GetComponent<BossTracker>();
        started = false;
    }

    void Update()
    {
        if (bts.deadBoss && !started)
        {
            started = true;
            scoreText.text = player.GetComponent<Player>().score.ToString();
            scoreText.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(bossSong);
            bossDeadSound.GetComponent<AudioSource>().Play();
            Destroy(bossDeadSound, 3f);
            victoryMusic.GetComponent<AudioSource>().PlayDelayed(3f);
        }
    }
}
