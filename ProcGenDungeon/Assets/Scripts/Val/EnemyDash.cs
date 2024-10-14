using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    [SerializeField] private AudioClip atkSound;
    [SerializeField] private AudioClip hurtSound;
    public GameObject player;
    public Player ps;
    public Vector3 moveDir;
    public Vector3 setMove;
    public float timePassed;
    public float moveSpeed;
    public int health;
    public Vector3 right;
    public Vector3 left;
    public Vector3 randDir;
    public bool agro;
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        timePassed = 0;
        right = new Vector3(6f, 6f, 0f);
        left = new Vector3(-6f, 6f, 0f);
        agro = false;
        randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        randDir = Vector3.Normalize(randDir);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ps.score += 100;
            Destroy(gameObject);
        }
        transform.GetChild(0).GetComponent<zomHealth>().hb = health;
        moveDir = player.transform.position - transform.position;
        if (agro)
        {
            timePassed += Time.deltaTime;

            if (timePassed < 1)
            {

                setMove = Vector3.Normalize(moveDir);
                moveSpeed = -0.5f;
                if (setMove.x > 0)
                {
                    transform.localScale = right;
                }
                else
                {
                    transform.localScale = left;
                }
            }
            else if (timePassed < 2)
            {
                moveSpeed = 5;
            }
            else if (timePassed < 4)
            {
                moveSpeed = 0.5f;

            }
            else
            {
                timePassed = 0;
            }
            transform.position += setMove * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (moveDir.magnitude < 3)
            {
                agro = true;
                timePassed = 0;
            }
            else
            {
                timePassed += Time.deltaTime;
                if (timePassed < 2)
                {
                    //do nothing
                }
                else if (timePassed < 3)
                {
                    moveSpeed = 3;
                    transform.position += randDir * moveSpeed * Time.deltaTime;
                }
                else
                {
                    randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    randDir = Vector3.Normalize(randDir);
                    timePassed = 0;
                }
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
            ps.currentHealth -= System.Math.Max(30 - ps.armor, 0);
            ps.healthBar.SetHealth(ps.currentHealth);
            SoundFX.instance.playSound(atkSound, transform, 1f);
        }

    }
}
