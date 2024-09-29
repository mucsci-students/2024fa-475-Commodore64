using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public GameObject player;
    public Vector3 moveDir;
    public float timePassed;
    public float moveSpeed;
    public int health; 
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        player = GameObject.Find("Player");
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
            moveSpeed = 0;
        }
        else{
            timePassed = 0;
        }
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        
    }
}
