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
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if(bts.deadBoss && !started){
            started = true;
            scoreText.text = player.GetComponent<Player>().score.ToString();
            Destroy(bossSong);
            bossDeadSound.GetComponent<AudioSource>().Play();
            Destroy(bossDeadSound, 3f);
            victoryMusic.GetComponent<AudioSource>().PlayDelayed(3f);
        }
        
    }
}
