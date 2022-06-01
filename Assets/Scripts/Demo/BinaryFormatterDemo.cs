using System;
using System.Collections;
using System.Collections.Generic;
using System.IO; //Para usar StreamWriters y StreamReaders
using System.Runtime.Serialization.Formatters.Binary;//Para poder usar los BinaryFormatter
using UnityEngine;
using UnityEngine.UI; //Para usar la UI


public class BinaryFormatterDemo : MonoBehaviour
{
    //Variable que yo puedo tener en mi juego
    private string _nameVariable = "Link";
    private float _hpVariable = 17.4f;
    private int _manaVariable = 99;
    private int _maxHPVariable = 20;

    //Esta clase podra Serializarse
    [System.Serializable]
    //Clase anidada contenedora de la información que queremos guardar
    private class DataContainer
    {
        //Referencias de las variables que queremos guardar
        public string _name;
        public float _hp;
        public int _mana;

        //Constructor de la clase al que le pasaremos los valores de las referencias
        public DataContainer(float hp, int mana, string name)
        {
            _hp = hp;
            _name = name;
            _mana = mana;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Write to File
        //Si pulsamos el botón S guardamos en el archivo de guardado
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Ruta del archivo de guardado
            string saveFilePath = Application.persistentDataPath + "/binaryformatter.sav";

            //Mostramos la ruta del archivo de guardado
            Debug.Log("Saving to: " + saveFilePath);

            //Creamos el DataContainer y le pasamos los valores que queremos guardar
            DataContainer data = new DataContainer(_hpVariable, _manaVariable, _nameVariable);

            //Creamos un BinaryFormatter que deriva de la clase principal
            BinaryFormatter bf = new BinaryFormatter();
            //Creamos un FileStream para generar el archivo de guardado
            FileStream fs = new FileStream(saveFilePath, FileMode.OpenOrCreate);
            //La instancia de la clase creada arriba es Serializada, en la ruta que queremos, con los datos que queremos
            bf.Serialize(fs, data);
            //Cerramos el archivo de guardado
            fs.Close();
        }

        //Si pulsamos el botón L cargamos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Ruta de donde queremos leer la información
            string saveFilePath = Application.persistentDataPath + "/binaryformatter.sav";
            //Creamos el Binary Formatter
            BinaryFormatter bf = new BinaryFormatter();
            //Hacemos referencia y abrimos el archivo de guardado
            FileStream fs = new FileStream(saveFilePath, FileMode.Open);
            //Deserializamos la información del archivo de guardado
            //bf.Deserialize(fs);
            //Creamos el DataContainer que contendrá la información del archivo de guardado
            DataContainer data = bf.Deserialize(fs) as DataContainer;
            //Imprimimos la información cargada por consola
            Debug.Log("hp: " + data._hp);
            Debug.Log("name: " + data._name);
            Debug.Log("mana: " + data._mana);
        }
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/savegame.sav
    */
}
