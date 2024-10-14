using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracker : MonoBehaviour
{
    public int bossesKilled;
    public bool deadBoss;
    // Start is called before the first frame update
    void Start()
    {
        bossesKilled = 0;
        deadBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        deadBoss = bossesKilled == 8;
    }
}
