using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class projectile : MonoBehaviour
{
    public GameObject player;
    public Player ps;
    public Vector3 offscreen;
    public Vector3 mousepos;
    public Vector3 setDir;
    public Vector3 deltaPos;
    public Vector3 scaling;
    public float movespeed;
    public bool isWarp;
    public bool overPit;

    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
        offscreen = new Vector3(0f, 0f, 0f);
        scaling = new Vector3(25f, 25f, 25f);
        transform.position = offscreen;
        transform.localScale = offscreen;
        movespeed = 0f;
        isWarp = false;
        overPit = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0f;
        mousepos = mousepos - player.transform.position;

        deltaPos = setDir * movespeed * Time.deltaTime;

        transform.position += deltaPos;

        if (Input.GetKeyDown(KeyCode.Q) && !ps.isDead)
        {
            if (isWarp)
            {
                if (ps.currentEnergy > 0)
                {
                    ps.energyBar.decreaceEnergy(10);
                    ps.currentEnergy = ps.energyBar.getEnergy();
                }

                if (!overPit)
                {
                    player.transform.position = transform.position;
                }
                teleOffScreen();
            }
            else
            {
                if (ps.currentEnergy > 0)
                {
                    movespeed = 10f;
                    setDir = Vector3.Normalize(mousepos);
                    isWarp = true;
                    transform.position = player.transform.position;
                    transform.localScale = scaling;
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            overPit = true;
        }
        else
        {
            teleOffScreen();
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
            overPit = false;
        }
    }

    void teleOffScreen()
    {
        transform.position = offscreen;
        movespeed = 0;
        isWarp = false;
        transform.localScale = offscreen;
    }


}
