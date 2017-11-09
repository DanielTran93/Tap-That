using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class StartGame : MonoBehaviour {

    public Text highScoreText;
    public static int highScore;
    public string leaderboard;
    bool isApp;


     void Start()
    {
        PlayerPrefs.GetInt("HighScore");
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "BEST TAP\n" + highScore;
        //PlayerPrefs.DeleteAll();
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        }
        );

    }


    void Update()
    {
        //transform.Rotate(0, 0, Random.Range(0,360) * Time.deltaTime);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("How To Play");
    }

    public void OpenLeaderBoard()

    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("TapThat");
    }

    public void OpenAchievements()
    {
        ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
    }

    public void Facebook()
    {
        
        Application.OpenURL("fb://page/649904945175469");
        StartCoroutine(CheckApp());
        isApp = false;
        //Application.OpenURL("https://www.facebook.com/649904945175469");

    }

    void OnApplicationPause()
    {
        isApp = true;
    }

    IEnumerator CheckApp()
    {
        // Wait for a time
        yield return new WaitForSeconds(1.5f);

        // If app hasn't launched, default to opening in browser
        if (!isApp)
        {
            Application.OpenURL("https://www.facebook.com/649904945175469");
        }
    }

}
