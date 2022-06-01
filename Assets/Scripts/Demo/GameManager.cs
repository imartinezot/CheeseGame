using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Librerías añadidas
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text puntosTXT;
    public Text nombreTXT;
    public GameObject panelGO;
    public GameObject rankingGO;
    int puntosDB;

    public GameObject GameManagerOriginal;

    public void GenerarPuntos()
    {
        int puntos = GameManagerOriginal.GetComponent<Puntuacion>().puntos;
        puntosDB = (int)puntos;
        puntosTXT.text = puntos.ToString();
    }

    public void ActivarPanel()
    {
        panelGO.SetActive(true);
    }

    public void GuardarPuntosDB()
    {
        rankingGO.GetComponent<RankingManager>().InsertarPuntos(nombreTXT.text, puntosDB);
    }
}
