using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class projectile : MonoBehaviour
{
    public GameObject player;
    public Vector3 offscreen;
    public Vector3 mousepos;
    public Vector3 setDir;
    public Vector3 deltaPos;
    public float movespeed;
    public bool isWarp;
    public bool overPit;
    
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.Find("Hero");
        offscreen = new Vector3 (-6f, 0f, 0f);
        transform.position = offscreen;
        movespeed = 0f;
        isWarp = false;
        overPit = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0f;

        deltaPos = setDir * movespeed * Time.deltaTime;

        transform.position += deltaPos;
        
        if (Input.GetMouseButtonDown(1)){
            if (isWarp){
                if(!overPit){
                    player.transform.position = transform.position;
                }
                teleOffScreen();
            }
            else{
                movespeed = 1f;
                setDir = Vector3.Normalize(mousepos);
                isWarp = true;
                transform.position = player.transform.position;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 11) {
            overPit = true;
        }
        else{
		    teleOffScreen();
        }
    
	}

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.layer == 11) {
            overPit = false;
        }
    }

    void teleOffScreen(){
        transform.position = offscreen;
        movespeed = 0;
        isWarp = false;
    }

    
}
