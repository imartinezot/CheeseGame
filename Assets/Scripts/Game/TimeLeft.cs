using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{
    public GameObject Game;
    public GameObject RankingGenerator;
    public GameObject RankingManager;

    public TextMeshProUGUI TimeLeftText;
    public float TimeLeftN = 5;
    public float TimeLeftNRound;

    public Image FillBar;
    public Image FillBar1;
    void Update()
    {
        //time left 5 seconds, restablish it through clicking cheese
        TimeLeftN = TimeLeftN - Time.deltaTime;
        //rounded to 1 decimal
        TimeLeftNRound = Mathf.Round(TimeLeftN * 10.0f) * 0.1f;
        //shown in UI
        TimeLeftText.SetText(TimeLeftNRound.ToString() + "s");

        //shown in the fill bar
        FillBar.fillAmount = TimeLeftN / 5;
        FillBar1.fillAmount = TimeLeftN / 5;

        //colors of the fillbar
        if (TimeLeftN >3.5f)
        {
            //verde //>70%
            FillBar.GetComponent<Image>().color = new Color32(134, 255, 142, 255);
            FillBar1.GetComponent<Image>().color = new Color32(134, 255, 142, 255);

        }
        if (TimeLeftN > 2f && TimeLeftN <3.5f)
        {
            //amarillo //>40%
            FillBar.GetComponent<Image>().color = new Color32(255, 253, 134, 255);
            FillBar1.GetComponent<Image>().color = new Color32(255, 253, 134, 255);
        }
        if (TimeLeftN < 2f)
        {
            //rojo //<40%
            FillBar.GetComponent<Image>().color = new Color32(255, 95, 93, 255);
            FillBar1.GetComponent<Image>().color = new Color32(255, 95, 93, 255);

        }

        if (TimeLeftN <= 0)
        {
            //Lose //pause game //activate scoreboard save
            //Time.timeScale = 0;
            Game.SetActive(false);
            RankingGenerator.SetActive(true);
            RankingManager.GetComponent<GameManager>().GenerarPuntos();
        }
    }
}
