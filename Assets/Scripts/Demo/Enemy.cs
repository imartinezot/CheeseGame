using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq; //Para poder usar Json.net y estructuras de datos
using System.IO; //Usar StreamWriter y StreamReader

public class Enemy : MonoBehaviour
{
    public int hp = 7;
    public string name = "Sasuke";

    //Crearemos un objeto serializable capaz de ser guardado
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

    //Write to File
    //Método para guardar la información
    void SaveGame()
    {
        //Guardamos en un string el contenido del script osea la instancia de este
        string jsonString = JsonUtility.ToJson(this);

        //Ruta donde queremos guardar la información
        string saveFilePath = Application.persistentDataPath + "/jsonSavingObjectsDemo.sav";
        //Creamos un StreamWriter para guardar la información en la ruta dada
        StreamWriter sw = new StreamWriter(saveFilePath);
        //Muestra la ruta del archivo por consola
        Debug.Log("Saving to: " + saveFilePath);
        //Escribimos la información que queremos en el archivo de guardado
        sw.WriteLine(jsonString);
        //Al acabar cerramos el StreamWriter
        sw.Close();
    }

    //Método para cargar la información del archivo de guardado
    void LoadGame()
    {
        //Ruta de donde queremos leer la información
        string saveFilePath = Application.persistentDataPath + "/jsonSavingObjectsDemo.sav";
        //Muestra la ruta del archivo por consola
        Debug.Log("Loading from: " + saveFilePath);

        //Creamos un StreamReader que nos permita leer la información del archivo de guardado
        StreamReader sr = new StreamReader(saveFilePath);
        //Creamos un string donde guardar la información que leemos
        string jsonString = sr.ReadToEnd();
        //Al acabar la lectura de datos cerramos el StreamReader
        sr.Close();
        //La información recibida del archivo de guardado sobreescribirá los campos oportunos del jsonString
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }
}
