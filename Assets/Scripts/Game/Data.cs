using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq; //Para poder usar Json.net y estructuras de datos
using System.IO; //Usar StreamWriter y StreamReader

public class Data : MonoBehaviour
{
    //time to reach for next cheese spawn
    public float SpawnTiming;
    //time left to lose
    public float TimeLeft;
    //cheese clicked
    public int Points;

    public JObject Serialize()
    {
        //Creamos un string que guardará el jSon
        string jsonString = JsonUtility.ToJson(this);
        //Creamos un objeto en el jSon
        JObject retVal = JObject.Parse(jsonString);
        //Al ser un método de tipo, debe devolver este tipo
        return retVal;
    }

    //Tendremos que deserializar la información recibida
    public void Deserialize(string jsonString)
    {
        //La información recibida del archivo de guardado sobreescribirá los campos oportunos del jsonString
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    //Overwrites variables of game with data from this script, this should be called after called load
    public void Load()
    {
        GameObject.Find("GameManager").GetComponent<TimeToSpawn>().SpawnTiming = SpawnTiming;
        GameObject.Find("GameManager").GetComponent<TimeLeft>().TimeLeftN = TimeLeft;
        GameObject.Find("GameManager").GetComponent<Puntuacion>().puntos = Points;
    }
    //Overwrites variables of this script with current game information,
    //this should be called just before saving data in order to update info of this script, wich is going to be saved
    public void Save()
    {
        SpawnTiming = GameObject.Find("GameManager").GetComponent<TimeToSpawn>().SpawnTiming;
        TimeLeft = GameObject.Find("GameManager").GetComponent<TimeLeft>().TimeLeftN;
        Points = GameObject.Find("GameManager").GetComponent<Puntuacion>().puntos;
    }
}
