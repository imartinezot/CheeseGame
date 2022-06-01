using System.Collections;
using System.Collections.Generic;
using System.IO; //Para usar StreamWriters y StreamReaders
using UnityEngine;
using UnityEngine.UI; //Para usar la UI

public class BinaryWriterDemo : MonoBehaviour
{
    private string _name = "Link";
    private int _playerLevel = 8;
    private float _hp = 17.4f;

    //Referencias a los textos de la UI
    public Text nameText, levelText, liveText;

    // Update is called once per frame
    void Update()
    {
        //Write to File
        //Si pulsamos el botón S guardamos en el archivo de guardado
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Ruta del archivo de guardado
            string saveFilePath = Application.persistentDataPath + "/binary.sav";

            //Mostramos la ruta del archivo de guardado
            Debug.Log("Saving to: " + saveFilePath);

            //Creamos un archivo de guardado en la ruta deseada, si ya existía en vez de crearlo lo abre
            FileStream fs = new FileStream(saveFilePath, FileMode.OpenOrCreate);
            //Creamos un BinaryWriter que actuará en la ruta deseada
            BinaryWriter bw = new BinaryWriter(fs);
            //Escribimos el nombre del jugador
            bw.Write(_name);
            //Escribimos el nivel del jugador
            bw.Write(_playerLevel);
            //Escribimos la vida del jugador
            bw.Write(_hp);
            //Cerramos el archivo de guardado
            fs.Close();
            //Cerramos el BinaryWriter
            bw.Close();


        }

        //Si pulsamos el botón L cargamos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Ruta de donde queremos leer la información
            string saveFilePath = Application.persistentDataPath + "/binary.sav";
            //Creamos una referencia al archivo de guardado que lo abrirá
            FileStream fs = new FileStream(saveFilePath, FileMode.Open);
            //Creamos un Binary Reader
            BinaryReader br = new BinaryReader(fs);
            //Mostramos por consola
            Debug.Log("Name: " + br.ReadString());
            Debug.Log("Player Level: " + br.ReadInt32());
            Debug.Log("HP: " + br.ReadSingle());
            //Cerramos el archivo de guardado
            fs.Close();
            //Cerramos el BinaryReader
            br.Close();

        }
    }

    /*Ruta del archivo de guardado
        C:/Users/User/AppData/LocalLow/DefaultCompany/DataPersistenceProject/savegame.sav
    */
}
