using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestartMenu : MonoBehaviour {

    public Text scoreText;
    public Text hightScoreText;

    void OnEnable()
    {
        scoreText.text = /*"Score \n" + */DataManager.instance.Score.ToString();
        hightScoreText.text = /*"Hight score \n" + */DataManager.instance.HightScore.ToString();
    }
}
