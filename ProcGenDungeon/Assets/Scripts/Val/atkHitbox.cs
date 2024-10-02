using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class atkHitbox : MonoBehaviour
{

    public GameObject player;
    public Player ps;
    public Vector3 mousepos;
    public Vector3 atkDir;
    public float atkAngle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        mousepos = ps.setMouse;
        

        atkDir = mousepos - player.transform.position;
        atkDir = Vector3.Normalize(atkDir);
        transform.position += atkDir;
        atkAngle = Vector3.Angle(new Vector3 (0f, 1f, 0f), atkDir);

        if(atkDir.x > 0){
            atkAngle = -atkAngle;   
        }

        transform.rotation = Quaternion.Euler(0f, 0f, atkAngle);
        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    

}