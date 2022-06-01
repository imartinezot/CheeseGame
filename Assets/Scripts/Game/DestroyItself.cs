using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    //chronometer
    float chronometer;
    //Time it destroys itsef
    float TimeToDie;

    void Update()
    {
        //time of its death
        TimeToDie = GameObject.Find("GameManager").GetComponent<TimeToSpawn>().SpawnTiming;
        //time alive so far
        chronometer = chronometer + Time.deltaTime;

        if (chronometer > TimeToDie)
        {
            death();
        }
    }
    public void clicked()
    {
        GameObject.Find("GameManager").GetComponent<Puntuacion>().puntos++;

        death();
    }

    void death()
    {
        Destroy(gameObject);
    }
}
