using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreTime : MonoBehaviour
{
    //time to reach
    public float SpawnTiming = 1.2f;
    public float ReduceSpawnTiming = 1.00002f;
    public void RestoreTimeClick()
    {
        SpawnTiming = SpawnTiming / ReduceSpawnTiming;
        GameObject.Find("GameManager").GetComponent<TimeLeft>().TimeLeftN += SpawnTiming;
    }
}
