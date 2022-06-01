using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Puntuacion : MonoBehaviour
{
    public TextMeshProUGUI PuntuacionText;
    public int puntos;
    void Update()
    {
        //PuntuacionObject.GetComponent<TextMeshPro>().SetText(puntuacion.ToString());

        PuntuacionText.SetText(puntos.ToString());

    }
}
