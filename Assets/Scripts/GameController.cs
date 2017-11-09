using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;




public class GameController : MonoBehaviour
{
    private InterstitialAd interstitial;
    //time
    public float timeLeft;
    //the gui time left text
    public Text timeText;
    //button on gui
    public Transform leftButton;
    public Transform rightButton;
    public Transform resetButton;
    //counts the times you have clicked
    public static int leftCounter;
    public static int rightCounter;
    //amount of times to click
    public static int leftAmountToTap;
    public static int rightAmountToTap;
    // shows on the game GUI
    public Text leftAmountToTapText;
    public Text rightAmountToTapText;
    //keeps count of current score
    public static int currentScore;
    //score for game over screen
    public static int score;
    //score text on gui
    public Text scoreText;
    //current high score
    public static int highScore;
    //high score text on GUI
    public Text highScoreText;
    //difficulty counter
    public int difficultyCounter;
    //audiosource on gui
    public AudioSource audioSource;
    //the fail sound clip if they are over tap count
    public AudioClip audioFailClip;
    //the sound for when they are not over tap count
    public AudioClip audioButtonPressed;
    //sound for when they pass
    public AudioClip audioPass;
    //sound for failure
    public AudioClip audioFailed;
    public Text countdownText;
    public GameObject timeResetText;
    public GameObject timeTextObject;
    public GameObject goodBye;
    public int buttonClickSound;
    public int loseSound;
    public int passSound;
    public GameObject boom;
    public GameObject audioLose;
    public GameObject audioLoseBoom;
    public static int blownUpBefore;
    public static int blownUpTimes;


    // Use this for initialization

    void Start()
    {

        leftCounter = 0;
        rightCounter = 0;
        currentScore = 0;
        difficultyCounter = 1;
        RandomAmountToTap();
        timeLeft = 10.5f;
        leftAmountToTapText.text = "" + leftAmountToTap;
        rightAmountToTapText.text = "" + rightAmountToTap;
        scoreText.text = "TAP SCORE\n" + currentScore;
        PlayerPrefs.GetInt("HighScore");
        highScore = PlayerPrefs.GetInt("HighScore");
        RequestInterstitial();
        buttonClickSound = AudioCenter.loadSound("ButtonClick");
        loseSound = AudioCenter.loadSound("Bomb Explode");
        passSound = AudioCenter.loadSound("Pass");
        PlayerPrefs.GetInt("BlownUpTimes");
        blownUpBefore = PlayerPrefs.GetInt("BlownUpTimes");


    }

    void Update()
    {

        score = currentScore;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }

        if (timeLeft < 9.5)
        {
            timeText.text = "TIME LEFT\n" + "00:0" + Mathf.RoundToInt(timeLeft);
        }

        else
        {
            timeText.text = "TIME LEFT\n" + "00:" + Mathf.RoundToInt(timeLeft);
        }

        Achievements();

        //if the time left less than 0 and the times clicked is less or above than the amount of timestoClick then you lose
        if (leftAmountToTap < 0 || rightAmountToTap < 0/*leftCounter > leftAmountToTap | rightCounter > rightAmountToTap*/)
        {
            //timeLeft = 0;
            Reset();

        }

        if (timeLeft <= 0)
        {
            Reset();

        }

    }

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        //timeText.text = "TIME LEFT\n" + "00:" + Mathf.RoundToInt(timeLeft);
        highScoreText.text = "" + highScore;

    }

    void OnGUI()
    {

        if (timeLeft < 2.5f && timeLeft > 0)
        {
            audioLose.SetActive(true);
        }



        //this is for the random number effect we see when waiting for new numbers.
        if (timeLeft > 10.5F)
        {
            RandomAmountToTap();
        }

        //when the time is above 0 and below 11, it will display the REAL amount needed to be tapped.
        if (timeLeft > 0 && timeLeft < 11)
        {
            leftAmountToTapText.text = "" + leftAmountToTap;
            rightAmountToTapText.text = "" + rightAmountToTap;
        }
        
    }

    public void Reset()
    {
        if (leftAmountToTap == 0 && rightAmountToTap == 0/*leftCounter == leftAmountToTap && rightCounter == rightAmountToTap*/)
        {
            //sets time to 11.5 seconds (gives user 1.5 seconds to get ready)
            timeLeft = 11.5f;
            //increase difficulty counter each time you are successful
            difficultyCounter++;
            //calls this method to generate more numbers.
            RandomAmountToTap();
            //unable to click buttons during this time as it will screw up the counters
            leftButton.GetComponent<Button>().interactable = false;
            rightButton.GetComponent<Button>().interactable = false;
            resetButton.GetComponent<Button>().interactable = false;
            //Enables the BOMB RESET text where time is. the object is hidden and then set to true so it can be shown.
            timeResetText.SetActive(true);
            //disables the timer text for the mean time
            timeTextObject.SetActive(false);
            //calls the method TapAgain()
            StartCoroutine(TapAgain());
            //updates score
            Score();
            //sets counters to 0 to start fresh
            leftCounter = 0;
            rightCounter = 0;
            //plays the fucking audio
            AudioCenter.playSound(passSound);
            //audioSource.clip = audioPass;
            //audioSource.Play();
        }
        
       else if (leftAmountToTap != 0 || rightAmountToTap != 0/*leftCounter != leftAmountToTap || rightCounter != rightAmountToTap*/)
        {

            //Brings up disabled images 
            boom.gameObject.SetActive(true);
            //Sets text on buttons to boom
            leftAmountToTapText.text = "BOOM!";
            rightAmountToTapText.text = "BOOM!";
            //sets the time to 0 so it does not go into negatives
            //timeLeft = 0;
            timeTextObject.SetActive(false);
            goodBye.SetActive(true);
            //calls restart method down below in 0.5 seconds
            Invoke("Restart", 0.8f);
            //makes the buttons unclickable
            leftButton.GetComponent<Button>().interactable = false;
            rightButton.GetComponent<Button>().interactable = false;
            resetButton.GetComponent<Button>().interactable = false;
            audioLoseBoom.SetActive(true);
            //AudioCenter.playSound(loseSound);
            Handheld.Vibrate();
        }
    }



    //this is used on the button on Click function. everytime a click is done the counter adds 1.
    public void leftTapCounter()
    {
        PlayButtonSound();
        leftAmountToTap--;
        leftCounter++;
    }
    public void rightTapCounter()
    {
        PlayButtonSound();
        rightAmountToTap--;
        rightCounter++;
    }

    //button sound when tapped this is used with the counter methods
    public void PlayButtonSound()
    {
        //soundID = AudioCenter.loadSound("ButtonClick");
        AudioCenter.playSound(buttonClickSound);
    }

    //sets the timestoClick amount to random range from 15 to 65 depending on the difficulty counter
    public void RandomAmountToTap()
    {
        if (difficultyCounter < 3)
        {
            leftAmountToTap = UnityEngine.Random.Range(5, 15);
            rightAmountToTap = UnityEngine.Random.Range(5, 15);
        }
        else if (difficultyCounter >= 3 && difficultyCounter < 5)           
        {
            leftAmountToTap = UnityEngine.Random.Range(10, 25);
            rightAmountToTap = UnityEngine.Random.Range(10, 25);
        }
        else if (difficultyCounter >= 5 && difficultyCounter < 7)
        {
            leftAmountToTap = UnityEngine.Random.Range(15, 45);
            rightAmountToTap = UnityEngine.Random.Range(15, 45);
        }
        else if (difficultyCounter >= 7 && difficultyCounter < 9  )
        {
            leftAmountToTap = UnityEngine.Random.Range(20, 55);
            rightAmountToTap = UnityEngine.Random.Range(20, 55);
        }
        else if (difficultyCounter >= 9)
        {
            leftAmountToTap = UnityEngine.Random.Range(20, 65);
            rightAmountToTap = UnityEngine.Random.Range(20, 65);
        }
    }
    
    //adds the score by adding the counters of left and right buttons
    public void Score()
    {
        //gets its current score and adds the 2 counters onto its current score
        currentScore = currentScore + leftCounter + rightCounter;
        scoreText.text = "TAP SCORE\n" + currentScore;
    }

    //the restart thing for when you lose
    public void Restart()
    {
        blownUpBefore++;
        if (blownUpBefore > blownUpTimes)
        {
            blownUpTimes = blownUpBefore;
            PlayerPrefs.SetInt("BlownUpTimes", blownUpTimes);
        }
        //loads the scene game over
        SceneManager.LoadScene("GameOver");
        //shows the interstitial ad
        ShowInterstitial();
    }

    //the method which makes you wait 1 second when you are successful
    IEnumerator TapAgain()
    {
        yield return new WaitForSeconds(1);
        timeResetText.SetActive(false);
        timeTextObject.SetActive(true);
        leftButton.GetComponent<Button>().interactable = true;
        rightButton.GetComponent<Button>().interactable = true;
        resetButton.GetComponent<Button>().interactable = true;
    }

    public void Achievements()
    {
        if (score >= 100)
        {
            Social.ReportProgress("CgkI5_StjrcWEAIQAg", 100.0f, (bool success) => { });
        }

        if (score >= 1000)
        {
            Social.ReportProgress("CgkI5_StjrcWEAIQBQ", 100.0f, (bool success) => { });
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

                .AddKeyword("game")
                .AddKeyword("fun")
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


