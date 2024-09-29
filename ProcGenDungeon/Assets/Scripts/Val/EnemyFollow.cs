using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerDir;
    public Vector3 moveDir;
    public Vector3 randDir;
    public float moveSpeed;
    public float timePassed;
    public bool agro;
    public int health; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        health = 100;
        timePassed = 0;
        moveSpeed = 1.5f;
        agro = false;
        randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = player.transform.position - transform.position; 

        if(!agro){
            if (playerDir.magnitude < 5){
                agro = true;
            }

            timePassed += Time.deltaTime;
            if(timePassed < 3f){
                moveDir = Vector3.zero;
            }
            else if (timePassed < 4){
                moveDir = randDir;
            }
            else{
                timePassed = 0;
                randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                randDir = Vector3.Normalize(randDir);
            }
        }
        else{
            moveDir = Vector3.Normalize(playerDir);
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
