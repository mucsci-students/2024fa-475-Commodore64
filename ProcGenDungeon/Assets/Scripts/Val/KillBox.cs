using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject player;
    public Player ps;

    void Start()
    {
        player = GameObject.Find("Hero");
        ps = player.GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ps.currentHealth -= System.Math.Max(9999999 - ps.armor, 0);
            ps.healthBar.SetHealth(ps.currentHealth);
        }
    }
}
