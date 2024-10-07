using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    public GameObject player;
    public GameObject cloneh;
    public GameObject cloneq;
    public GameObject clonee;
    public Player ps;
    public Vector3 playerDir;
    public Vector3 setDir;
    public int health;
    public bool half;
    public bool quarter;
    public bool eigth;
    public bool changeDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        
        health = 400;
        half = false;
        quarter = false;
        eigth = false;
        
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

        if (health <= 300 && !half){
            splith();
            half = true;
        } 
        else if (health <= 200 && !quarter){
            splitq();
            quarter = true;
        }
        else if (health <= 100 && !eigth){
            splite();
            eigth = true;
        } 
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 12) {
            health -= ps.damage;
        }
        else if (other.gameObject.layer == 0){
            changeDir =  true;
        }
	}
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == 9 && !ps.invulne) {
            ps.invulne = true;
            ps.health -= System.Math.Max(100 - ps.armor, 0);
        }

	}
    void splith(){
        Instantiate(cloneh, transform.position, Quaternion.identity);
    }
    void splitq(){
        Instantiate(cloneq, transform.position, Quaternion.identity);
    }
    void splite(){
        Instantiate(clonee, transform.position, Quaternion.identity);
    }
}