using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text highScoreText;
    public int highScore;

    void Start()
    {

        PlayerPrefs.GetInt("HighScore");
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "BEST TAP\n" + highScore;

    }
}
