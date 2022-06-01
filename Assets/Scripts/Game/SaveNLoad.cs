using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Usar StreamWriter y StreamReader
using Newtonsoft.Json.Linq; //Para poder usar Json.net y estructuras de datos
using System.Security.Cryptography; //Liber�a para encriptaci�n y desencriptaci�n de informaci�n

public class SaveNLoad : MonoBehaviour
{
    public Data[] Datas;

    public void SaveClicked()
    {
        //Generamos un jObject para guardar la informaci�n serializada m�s abajo
        JObject jSaveGame = new JObject();

        //Combinamos los objetos serializados (es decir los jSonString generados) y lo guardamos en el archivo de guardado
        for (int i = 0; i < Datas.Length; i++)
        {
            //actualizamos informacion en los script que contienen las variables a guardar
            Datas[i].Save();
            //Guardamos en una referencia el enemigo actual que estamos leyendo
            Data curData = Datas[i];
            
            //Generamos un jObject pasandole el enemigo concreto serializado
            JObject serializedData = curData.Serialize();
            //En el objecto jSon archivo de guardado, a�adimos la informaci�n que queremos de los objetos serializados
            jSaveGame.Add(curData.name, serializedData);
            Debug.Log("Saving " + jSaveGame);
        }

        //Ruta donde queremos guardar la informaci�n
        string saveFilePath = Application.persistentDataPath + "/jsonSave.sav";

        /*Para guardar la informaci�n en el archivo de guardado sin que est� encriptada*/
        //Creamos un StreamWriter para guardar la informaci�n en la ruta dada
        //StreamWriter sw = new StreamWriter(saveFilePath);
        //Muestra la ruta del archivo por consola
        //Debug.Log("Saving to: " + saveFilePath);
        //Escribimos la informaci�n que queremos en el archivo de guardado
        //sw.WriteLine(jSaveGame.ToString());
        //Al acabar cerramos el StreamWriter
        //sw.Close();

        //Creamos un array de bytes para guardar el array que nos devuelve el m�todo Encrypt para que pueda ser usado
        byte[] encryptSavegame = Encrypt(jSaveGame.ToString());
        //Escribimos esta informaci�n en el archivo de guardado, ya encriptada la informaci�n en su ruta 
        File.WriteAllBytes(saveFilePath, encryptSavegame);
        //Muestra la ruta del archivo por consola
        Debug.Log("Saving to: " + saveFilePath);
    }

    public void LoadClicked()
    {
        //Ruta de donde queremos leer la informaci�n
        string saveFilePath = Application.persistentDataPath + "/jsonSave.sav";
        //Muestra la ruta del archivo por consola
        Debug.Log("Loading from: " + saveFilePath);

        /*PARA CARGAR LA INFORMACI�N NO ENCRIPTADA*/
        //Creamos un StreamReader que nos permita leer la informaci�n del archivo de guardado
        //StreamReader sr = new StreamReader(saveFilePath);
        //Creamos un string donde guardar la informaci�n que leemos
        //string jsonString = sr.ReadToEnd();
        //Al acabar la lectura de datos cerramos el StreamReader
        //sr.Close();

        //Creamos un array con la informaci�n encriptada recibida
        byte[] decryptedSavegame = File.ReadAllBytes(saveFilePath);
        //Creamos un array donde guardar la informaci�n desencriptada recibida
        string jsonString = Decrypt(decryptedSavegame);

        //Generamos un jObject al que le pasamos la informaci�n del jSon
        JObject jSaveGame = JObject.Parse(jsonString);

        for (int i = 0; i < Datas.Length; i++)
        {
            //Cargamos en una referencia el enemigo actual que estamos leyendo
            Data curData = Datas[i];
            //Generamos un string para cargar la informaci�n sacada del archivo de guardado para esa instancia
            string DataJsonString = jSaveGame[curData.name].ToString();
            //Llamamos al m�todo que deserializa la informaci�n obtenida
            curData.Deserialize(DataJsonString);
            Datas[i].Load();
            Debug.Log("Loaded " + DataJsonString);
            
        }

    }

    /*PARA ENCRIPTAR Y DESENCRIPTAR LA INFORMACI�N DEL ARCHIVO DE GUARDADO
 */

    //Clave generada para la encriptaci�n en formato bytes, 16 posiciones
    byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    //Vector de inicializaci�n para la clave
    byte[] _initializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    //Encriptamos los datos del archivo de guardado que le pasaremos en un string
    byte[] Encrypt(string message)
    {
        //Usamos esta librer�a que nos permitir� a trav�s de una referencia crear un encriptador de la informaci�n
        AesManaged aes = new AesManaged();
        //Para usar este encriptador le pasamos tanto la clave como el vector de inicializaci�n que hemos creado nosotros arriba
        ICryptoTransform encryptor = aes.CreateEncryptor(_key, _initializationVector);
        //Lugar en memoria donde guardamos la informaci�n encriptada
        MemoryStream memoryStream = new MemoryStream();
        //Con esta referencia podremos escribir en el MemoryStream de arriba la informaci�n ya encriptada usando el encriptador con sus claves que ya hab�amos creado
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        //Con el StreamWriter podemos escribir en el archivo la informaci�n encriptada, que se habr� guardado en el MemoryStream
        StreamWriter streamWriter = new StreamWriter(cryptoStream);

        //Usando todo lo anterior, guardamos en el archivo de guardado el json que le pasamos por par�metro, haciendo el siguiente proceso: recibimos el string, lo encriptamos, queda guardado en la memoria reservada para la encriptaci�n
        streamWriter.WriteLine(message);

        //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupci�n del archivo o de la propia encriptaci�n
        streamWriter.Close();
        cryptoStream.Close();
        memoryStream.Close();

        //Por �ltimo el m�todo devolver� esta informaci�n que reside en el hueco de memoria con la informaci�n encriptada, convertida esta informaci�n en array de bytes
        return memoryStream.ToArray();
    }

    //Generamos un m�todo que nos devuelva la informaci�n del archivo de guardado desencriptada
    string Decrypt(byte[] message)
    {
        //Usamos esta librer�a que nos permitir� a trav�s de una referencia crear un desencriptador de la informaci�n
        AesManaged aes = new AesManaged();
        //Para usar este desencriptador le pasamos tanto la clave como el vector de inicializaci�n que hemos creado nosotros arriba
        ICryptoTransform decrypter = aes.CreateDecryptor(_key, _initializationVector);
        //Lugar en memoria donde guardamos la informaci�n desencriptada
        MemoryStream memoryStream = new MemoryStream(message);
        //Con esta referencia podremos escribir en el MemoryStream de arriba la informaci�n ya desencriptada usando el desencriptador con sus claves que ya hab�amos creado
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
        //Con el StreamReader podemos leer del archivo la informaci�n desencriptada, que se habr� guardado en el MemoryStream
        StreamReader streamReader = new StreamReader(cryptoStream);

        //Usando todo lo anterior, cargamos del archivo de guardado el json que le pasamos por par�metro, haciendo el siguiente proceso: recibimos el string, lo desencriptamos, queda guardado en la memoria reservada para la desencriptaci�n
        string decryptedMessage = streamReader.ReadToEnd();

        //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupci�n del archivo o de la propia encriptaci�n
        streamReader.Close();
        cryptoStream.Close();
        memoryStream.Close();

        //Por �ltimo el m�todo devolver� esta informaci�n que reside en el hueco de memoria con la informaci�n desencriptada, convertida esta en un string
        return decryptedMessage;
    }
}
