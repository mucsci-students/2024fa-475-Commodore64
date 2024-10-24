using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracker : MonoBehaviour
{
    public int bossesKilled;
    public bool deadBoss;
    public bool done;

    void Start()
    {
        bossesKilled = 0;
        deadBoss = false;
        done = false;
    }

    void Update()
    {
        deadBoss = bossesKilled == 8;
        if (deadBoss && !done)
        {
            done = true;
            Destroy(GameObject.Find("Skeleton Teleporter Spawner"));
            GameObject[] skeles = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject skele in skeles)
                Destroy(skele);
        }
    }
}
