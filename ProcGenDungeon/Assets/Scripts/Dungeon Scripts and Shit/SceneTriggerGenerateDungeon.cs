using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneTriggerGenerateDungeon : CorridorFirstDungeonGeneration
{

    void Start()
    {
        tilemapVisualizer.Clear();
        CorridorFirstGeneration();
    }

}
