using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossHalf : MonoBehaviour
{
    public GameObject player;
    public GameObject cloneq;
    public GameObject clonee;
    public Vector3 playerDir;
    public Vector3 setDir;
    public Player ps;
    public int health;
    public bool quarter;
    public bool eigth;
    public bool changeDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();

        health = 300;
        quarter = false;
        eigth = false;

        changeDir = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
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

        if (health <= 200 && !quarter)
        {
            splitq();
            quarter = true;
        }
        else if (health <= 100 && !eigth)
        {
            splite();
            eigth = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            health -= ps.damage;
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
        }

    }

    void splitq()
    {
        Instantiate(cloneq, transform.position, Quaternion.identity);
    }
    void splite()
    {
        Instantiate(clonee, transform.position, Quaternion.identity);
    }
}
