using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    [SerializeField] private AudioClip atkSound;
    [SerializeField] private AudioClip teleSound;
    [SerializeField] private AudioClip hurtSound;
    public GameObject player;
    public Player ps;
    public GameObject telepoint;
    public Vector3 dest;
    public float timePassed;
    public int health;
    public Vector3 right;
    public Vector3 left;
    public Vector3 playerDir;
    public bool agro;

    // Start is called before the first frame update
    void Start()
    {
        health = 200;
        dest = transform.position;
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        timePassed = 0;
        right = new Vector3(9f, 9f, 0f);
        left = new Vector3(-9f, 9f, 0f);
        agro = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ps.score += 300;
            Destroy(gameObject);
        }
        transform.GetChild(0).GetComponent<zomHealth>().hb = health;
        playerDir = player.transform.position - transform.position;
        if (agro)
        {
            if (playerDir.x > 0)
            {
                transform.localScale = right;
            }
            else
            {
                transform.localScale = left;
            }

            timePassed += Time.deltaTime;
            if (timePassed < 1)
            {
                // do nothing
            }
            else if (timePassed < 2)
            {
                transform.position = dest;
                
            }
            else
            {
                SoundFX.instance.playSound(teleSound, transform, .5f);
                timePassed = 0;
                Instantiate(telepoint, player.transform.position, Quaternion.identity);
                dest = player.transform.position;
            }
        }
        else
        {
            if (playerDir.magnitude < 8)
            {
                agro = true;
                timePassed = 0;
            }
        }
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
            ps.invulne = true;
            ps.currentHealth -= System.Math.Max(80 - ps.armor, 0);
            ps.healthBar.SetHealth(ps.currentHealth);
            SoundFX.instance.playSound(atkSound, transform, 1f);
        }

    }
}
