using System.Collections;
using System.Collections.Generic;
using System.IO; //Para usar StreamWriters y StreamReaders
using UnityEngine;
using UnityEngine.UI; //Para usar la UI


public class StreamWriterDemo : MonoBehaviour
{

    private string _name = "Link";
    private int _playerLevel = 8;
    private float _hp = 17.4f;

    //Referencias a los textos de la UI
    public Text nameText, levelText, liveText;

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
            //Ruta del archivo de guardado
            string saveFilePath = Application.persistentDataPath + "/zelda.sav";

            //Mostramos la ruta del archivo de guardado
            Debug.Log("Saving to: " + saveFilePath);

            //Creamos el StreamWriter que genera el archivo de guardado en esa ruta
            StreamWriter sw = new StreamWriter(saveFilePath);
            //Escribimos en el archivo de guardado
            sw.WriteLine(_name);
            //Escribimos en el archivo de guardado
            sw.WriteLine(_playerLevel);
            //Escribimos en el archivo de guardado
            sw.WriteLine(_hp);
            //Una vez hayamos terminado, cerramos el StreamWriter
            sw.Close();
        }

        //Si pulsamos el botón L cargamos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Ruta de donde queremos leer la información
            string saveFilePath = Application.persistentDataPath + "/zelda.sav";
            //Para leer la información generamos un StreamReader en esa localización
            StreamReader sr = new StreamReader(saveFilePath);
            //Guardaremos el resultado de lo leído en un string
            //string line = sr.ReadLine();
            //Ponemos en UI el resultado de la lectura
            nameText.text = sr.ReadLine();
            levelText.text = sr.ReadLine().ToString();
            liveText.text = sr.ReadLine().ToString();
            //Al terminar cerramos el StreamReader
            sr.Close();
        }
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/savegame.sav
    */
}
