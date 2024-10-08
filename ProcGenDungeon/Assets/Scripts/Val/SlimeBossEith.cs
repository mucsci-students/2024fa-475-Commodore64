using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossEith : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerDir;
    public Vector3 setDir;
    public Player ps;
    public int health;
    public bool changeDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        
        health = 100;

        changeDir = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            Destroy(gameObject);
        }
        transform.localScale = new Vector3 (health/20f, health/20f, 0);

        playerDir = player.transform.position - transform.position;
        if(changeDir){
            setDir = Vector3.Normalize(playerDir);
            changeDir = false;
        }
        transform.position += setDir * 5 * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 12) {
            health -= ps.damage;
        }
        else if (other.gameObject.layer == 0 || other.gameObject.layer == 10 || other.gameObject.layer == 6){
            changeDir =  true;
        }
	}
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == 9 && !ps.invulne) {
            ps.invulne = true;
            ps.health -= System.Math.Max(100 - ps.armor, 0);
        }

	}
}
