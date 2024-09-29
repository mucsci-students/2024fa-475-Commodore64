using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    public GameObject player;
    public GameObject telepoint;
    public Vector3 dest;
    public float timePassed;
    public int health; 

    // Start is called before the first frame update
    void Start()
    {
        health = 200;
        dest = transform.position;
        player = GameObject.Find("Player");
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed < 1){
            // do nothing
        }
        else if (timePassed < 2){
            transform.position = dest;
        }
        else{
            timePassed = 0;
            Instantiate(telepoint, player.transform.position, Quaternion.identity);
            dest = player.transform.position;
        }
    }
}
