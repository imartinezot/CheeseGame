using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Para usar la interfaz

public class SaveLoad : MonoBehaviour
{
    //Vida del jugador
    public int vida;
    //Referencia al texto de la UI
    public Text liveText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Al pulsar el botón S guardaremos la vida del jugador
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Guardo la vida en el archivo de guardado
            PlayerPrefs.SetInt("vida", vida);
        }

        //Al pulsar el botón L cargaremos la vida del jugador
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Comprobamos que la Key existe
            if (PlayerPrefs.HasKey("vida"))
            {
                //Cargamos la vida del jugador en el texto de la UI
                liveText.text = PlayerPrefs.GetInt("vida").ToString();
            }
        }

        //Al pulsar el botón C borramos el archivo de guardado
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Borro el archivo de guardado
            PlayerPrefs.DeleteAll();
        }
    }
}
