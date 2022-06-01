using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToSpawn : MonoBehaviour
{
    //chronometer
    public float SpawnTime;
    //time to reach
    public float SpawnTiming = 1.2f;
    public float ReduceSpawnTiming = 1.00002f;
    void Update()
    {
        SpawnTime = SpawnTime + Time.deltaTime;

        if (SpawnTime > SpawnTiming)
        {
            //botton spawn //access to singleton
            GameObject.Find("Randomizer").GetComponent<RandomPos>().RandomPosition();
            GameObject.Find("Randomizer").GetComponent<RandomPos>().SpawnObject();
            SpawnTime = 0;
        }
        //SpawnTiming = SpawnTiming - ReduceSpawnTiming;
        //ReduceSpawnTiming *= 0.9999f;
        SpawnTiming = SpawnTiming / ReduceSpawnTiming;
    }
}
