using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Usar StreamWriter y StreamReader
using Newtonsoft.Json.Linq; //Para poder usar Json.net y estructuras de datos

public class JsonDemo : MonoBehaviour
{
    //Variable que yo puedo tener en mi juego
    public string nameVariable;
    public float hpVariable;
    public string[] friends;
    // Update is called once per frame
    void Update()
    {
        //Write to File
        //Si pulsamos el botón S guardamos en el archivo de guardado
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Ruta donde queremos guardar la información
            string saveFilePath = Application.persistentDataPath + "/jsonDemo.sav";
            //Muestra la ruta del archivo por consola
            Debug.Log("Saving to: " + saveFilePath);
            //Creamos un objeto para el jSon
            JObject jObject = new JObject();
            //Añadimos al objeto la información que queremos (la propiedad)
            jObject.Add("componentName", GetType().ToString());
            //Vuelvo a crear un objeto que estará dentro del objeto jSon principal
            JObject jDataObject = new JObject();
            //Inscribimos este objeto dentro del principal
            jObject.Add("data", jDataObject);
            //Añadimos al objeto la información que queremos (la propiedad)
            jDataObject.Add("_name", nameVariable);
            jDataObject.Add("_curHp", hpVariable);
            //Creamos un array de tipo jSon usando el contenido del array friends
            JArray jFriendsArray = JArray.FromObject(friends);
            //Añadimos al objeto un array de datos
            jDataObject.Add("_friends", jFriendsArray);
            //Para comprobar finalmente le pedimos que nos muestre por consola el jSon final que quedaría
            Debug.Log(jObject.ToString());
            //Creamos un StreamWriter para guardar la información en la ruta dada
            StreamWriter sw = new StreamWriter(saveFilePath);
            //Escribimos la información que queremos en el archivo de guardado
            sw.WriteLine(jObject.ToString());
            //Al acabar cerramos el StreamWriter
            sw.Close();

        }

        //Si pulsamos el botón L cargamos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Ruta de donde queremos leer la información
            string saveFilePath = Application.persistentDataPath + "/jsonDemo.sav";
            //Muestra la ruta del archivo por consola
            Debug.Log("Loading from: " + saveFilePath);

            //Creamos un StreamReader que nos permita leer la información del archivo de guardado
            StreamReader sr = new StreamReader(saveFilePath);
            //Creamos un string donde guardar la información que leemos
            string jsonString = sr.ReadToEnd();
            //Al acabar la lectura de datos cerramos el StreamReader
            sr.Close();

            //Convertimos el string que le pasamos en un objeto
            JObject jObj = JObject.Parse(jsonString);

            //Mostramos por pantalla la información obtenida
            //Debug.Log(jsonString);
            //Mostramos la información de ComponentName que es una propiedad
            //Debug.Log("Component name: " + jObj["componentName"]);
            //Mostramos la información de un objeto dentro del json (sus propiedades)
            //Debug.Log(jObj["data"]["_name"]);

            //Obtenemos el valor del tipo que sea (en este caso string) y lo guardamos en una variable del mismo tipo
            nameVariable = jObj["data"]["_name"].Value<string>();
            hpVariable = jObj["data"]["_curHp"].Value<int>();
            friends = jObj["data"]["_friends"].ToObject<string[]>();

        }
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/savegame.sav
    */

}
