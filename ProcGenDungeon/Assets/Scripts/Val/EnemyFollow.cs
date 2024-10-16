using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private AudioClip atkSound;
    [SerializeField] private AudioClip hurtSound;
    public GameObject player;
    public Player ps;
    public Vector3 playerDir;
    public Vector3 moveDir;
    public Vector3 randDir;
    public float moveSpeed;
    public float timePassed;
    public bool agro;
    public int health;
    public bool hit;
    public Vector3 right;
    public Vector3 left;

    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();

        right = new Vector3(10f, 10f, 0f);
        left = new Vector3(-10f, 10f, 0f);
        health = 100;
        timePassed = 0;
        moveSpeed = 5f;
        agro = false;
        hit = false;
        randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }

    void Update()
    {
        playerDir = player.transform.position - transform.position;
        if (playerDir.x > 0)
        {
            transform.localScale = right;
        }
        else
        {
            transform.localScale = left;
        }

        if (health <= 0)
        {
            ps.score += 200;
            Destroy(gameObject);
        }
        transform.GetChild(0).GetComponent<zomHealth>().hb = health;

        if (!agro)
        {
            if (playerDir.magnitude < 5)
            {
                agro = true;
                timePassed = 0;
            }

            timePassed += Time.deltaTime;
            if (timePassed < 3f)
            {
                moveDir = Vector3.zero;
            }
            else if (timePassed < 5f)
            {
                moveDir = randDir;
            }
            else
            {
                timePassed = 0;
                randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                randDir = Vector3.Normalize(randDir);
            }
        }
        else if (hit)
        {
            timePassed += Time.deltaTime;
            if (timePassed < 3)
            {
                moveDir = -playerDir;
            }
            else
            {
                hit = false;
                timePassed = 0;
            }
        }
        else
        {
            moveDir = playerDir;
        }
        transform.position += Vector3.Normalize(moveDir) * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            health -= ps.damage;
            SoundFX.instance.playSound(hurtSound, transform, .5f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 9 && !ps.invulne)
        {
            hit = true;
            ps.invulne = true;
            ps.currentHealth -= System.Math.Max(50 - ps.armor, 0);
            ps.healthBar.SetHealth(ps.currentHealth);
            SoundFX.instance.playSound(atkSound, transform, 1f);
        }

    }
}
