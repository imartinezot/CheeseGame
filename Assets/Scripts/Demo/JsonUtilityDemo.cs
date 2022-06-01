using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Usar StreamWriter y StreamReader

public class JsonUtilityDemo : MonoBehaviour
{
    //Variables para el archivo de guardado
    public int hp = 7;
    public string name = "Naruto";
    //La variable seguirá siendo privada, pero accesible, tanto para jSon, como para el editor de Unity. Con HideInInspector no será accesible desde el editor de Unity.
    [SerializeField][HideInInspector] private int _age = 32;
    //Variable que seguirá siendo pública y accesible pero no se guardará en nuestro archivo, al especificarle que no será serializable
    [System.NonSerialized] public int damage = 10;
    public int maxHp = 10;

    public class SaveData
    {
        //Variables para serializar
        public int hp;
        public string name;
        public int age;

        //Constructor de la clase
        public SaveData(int _hp, string _name, int _age)
        {
            //Rellenamos las variables con las que le pasamos por parámetro
            hp = _hp;
            name = _name;
            age = _age;

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Write to File
        //Si pulsamos el botón S guardamos en el archivo de guardado
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Instanciamos la clase anidada pasándole por parámetro las variables que queremos guardar
            SaveData sd = new SaveData(hp, name, _age);

            //Guardamos en un string el contenido del script osea la instancia de este
            string jsonString = JsonUtility.ToJson(sd);

            //Ruta donde queremos guardar la información
            string saveFilePath = Application.persistentDataPath + "/jsonUtilityDemo.sav";
            //Creamos un StreamWriter para guardar la información en la ruta dada
            StreamWriter sw = new StreamWriter(saveFilePath);
            //Muestra la ruta del archivo por consola
            Debug.Log("Saving to: " + saveFilePath);
            //Escribimos la información que queremos en el archivo de guardado
            sw.WriteLine(jsonString);
            //Al acabar cerramos el StreamWriter
            sw.Close();
        }

        //Si pulsamos el botón L cargamos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Ruta de donde queremos leer la información
            string saveFilePath = Application.persistentDataPath + "/jsonUtilityDemo.sav";
            //Muestra la ruta del archivo por consola
            Debug.Log("Loading from: " + saveFilePath);

            //Creamos un StreamReader que nos permita leer la información del archivo de guardado
            StreamReader sr = new StreamReader(saveFilePath);
            //Creamos un string donde guardar la información que leemos
            string jsonString = sr.ReadToEnd();
            //Al acabar la lectura de datos cerramos el StreamReader
            sr.Close();
            //Instanciamos la clase anidada para cargar las variables de esta
            //La información recibida del archivo de guardado sobreescribirá los campos oportunos del jsonString
            SaveData sd = JsonUtility.FromJson<SaveData>(jsonString);
            //Realmente cargamos la información del archivo de guardado en las variables de Unity
            hp = sd.hp;
            name = sd.name;
            _age = sd.age;
        }
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/savegame.sav
    */

}
