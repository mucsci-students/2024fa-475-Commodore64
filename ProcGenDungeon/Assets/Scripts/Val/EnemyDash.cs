using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public GameObject player;
    public Player ps;
    public Vector3 moveDir;
    public float timePassed;
    public float moveSpeed;
    public int health; 
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        player = GameObject.Find("Player");
        ps = player.GetComponent<Player>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            Destroy(gameObject);
        }

        timePassed += Time.deltaTime;
        
        if (timePassed < 1){
            moveDir = player.transform.position - transform.position;
            moveDir = Vector3.Normalize(moveDir);
            moveSpeed = -0.5f;
        }
        else if (timePassed < 2){
            moveSpeed = 5;
        }
        else if (timePassed < 4){
            moveSpeed = -0.5;
        }
        else{
            timePassed = 0;
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 12) {
            health -= ps.damage;
        }
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == 9 && !ps.invulne) {
            ps.invulne = true;
            ps.health -= System.Math.Max(30 - ps.armor, 0);
        }

	}
}
