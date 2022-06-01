using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfGame : MonoBehaviour
{
    //Contador de tiempo
    public float TiempoJuego;

    void Update()
    {
        TiempoJuego = (TiempoJuego + Time.deltaTime);
    }
}
