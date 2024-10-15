using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossEith : MonoBehaviour
{
    [SerializeField] private AudioClip atkSound;
    [SerializeField] private AudioClip hurtSound;
    public GameObject player;
    public GameObject tracker;
    public Vector3 playerDir;
    public Vector3 setDir;
    public Player ps;
    public BossTracker ts;
    public int health;
    public bool changeDir;

    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();

        tracker = GameObject.Find("BossTracker");
        ts = tracker.GetComponent<BossTracker>();

        health = 100;

        changeDir = true;
    }

    void Update()
    {
        if (health <= 0)
        {
            ts.bossesKilled += 1;
            ps.score += 500;
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(health / 20f, health / 20f, 0);

        playerDir = player.transform.position - transform.position;
        if (changeDir)
        {
            setDir = Vector3.Normalize(playerDir);
            changeDir = false;
        }
        transform.position += setDir * 5 * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            health -= ps.damage;
            SoundFX.instance.playSound(hurtSound, transform, .5f);
        }
        else if (other.gameObject.layer == 0 || other.gameObject.layer == 10 || other.gameObject.layer == 6)
        {
            changeDir = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 9 && !ps.invulne)
        {
            ps.invulne = true;
            ps.currentHealth -= System.Math.Max(100 - ps.armor, 0);
            ps.healthBar.SetHealth(ps.currentHealth);
            SoundFX.instance.playSound(atkSound, transform, 1f);
        }

    }
}
