using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Restart : MonoBehaviour {

    private InterstitialAd interstitial;
    public Text scoreText;
    public Text highScoreText;
    int highScore;
    public string leaderboard;
    bool isApp;
    public Text blownUp;
    int blownUpTimes;


    void Start()
    {

        PlayerPrefs.GetInt("HighScore");
        highScore = PlayerPrefs.GetInt("HighScore");
        scoreText.text = "TAP SCORE\n" + GameController.score;
        highScoreText.text = "BEST TAP\n" + highScore;
        PlayerPrefs.GetInt("BlownUpTimes");
        blownUpTimes = PlayerPrefs.GetInt("BlownUpTimes");
        blownUp.text = "TOTAL EXPLOSIONS\n " + blownUpTimes;
        Achievements();

    }
    public void StartLevel()
    {
        SceneManager.LoadScene("TapThat");
        //ShowInterstitial();
    }

    public void OpenLeaderBoard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(highScore, leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }

            });
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
        }
    }

    public void OpenAchievements()
    {
        if (Social.localUser.authenticated)
        {
            ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
        }
    }

    public void Achievements()
    {
        if (blownUpTimes >= 10)
        {
            Social.ReportProgress("CgkI5_StjrcWEAIQAw", 100.0f, (bool success) => { });
        }

        if (blownUpTimes >= 100)
        {
            Social.ReportProgress("CgkI5_StjrcWEAIQBA", 100.0f, (bool success) => { });
        }
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





    // ------------------------------ ADS -----------------------------------------

    private void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-5748059298645580/2205190354";
#elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
            string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

    private void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            print("Interstitial is not ready yet.");
        }
    }

    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()

                .Build();
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion

}
