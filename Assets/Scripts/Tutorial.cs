using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Tutorial : MonoBehaviour {
    public Text timeText;
    public Text timeReset;
    public int tapLeft;
    public int tapRight;
    public int leftCounter = 5;
    public int rightCounter = 3;
    public Text tapScore;
    public GameObject timeObject;
    public GameObject timeResetObject;
    public GameObject disableLeft;
    public GameObject disableRight;
    public Text tapAmountLeft;
    public Text tapAmountRight;
    public GameObject play;
    public Transform left;
    public Transform right;
    public float timeLeft;
    public GameObject tap3Text;
    public GameObject pressResetText;
    public GameObject tap5Text;
    public int leftAmount;
    public int rightAmount;

    // Use this for initialization

    void Start ()
    {
        timeLeft = 9.5f;
        AmountToTap();
        tapAmountLeft.text = "" + leftAmount;
        tapAmountRight.text = "" + rightAmount;
    }
	
	// Update is called once per frame

	void Update ()

    {
        tapAmountLeft.text = "" + leftAmount;
        tapAmountRight.text = "" + rightAmount;

        if (timeLeft <= 0)

        {

            disableLeft.SetActive(true);
            disableRight.SetActive(true);
            left.GetComponent<Button>().interactable = false;
            right.GetComponent<Button>().interactable = false;
            tapAmountLeft.text = "BOOM";
            tapAmountRight.text = "BOOM";
            timeObject.SetActive(true);
            timeText.text = "GAME OVER";
            StartCoroutine(CountdownLose());
            tapLeft = 0;
            tapRight = 0;
            AmountToTap();
            
        }


        if (leftAmount < 0 | rightAmount < 0)

        {
            timeLeft = 0;
        }

        if (leftAmount <= 5)
        {
            tap5Text.SetActive(true);
            tap3Text.SetActive(true);
        }

        if (leftAmount == 0)
        {
            tap5Text.SetActive(false);
            tap3Text.SetActive(true);
        }

        if (leftAmount == 0)
        {
            tap5Text.SetActive(false);
            tap3Text.SetActive(true);
        }

        if (rightAmount == 0 && leftAmount == 0)
        {
            tap5Text.SetActive(false);
            tap3Text.SetActive(false);
            pressResetText.SetActive(true);
        }

    }

    public void Reset()

    {
        if (leftAmount == 0 && rightAmount == 0)

        {
            left.GetComponent<Button>().interactable = false;
            right.GetComponent<Button>().interactable = false;
            timeResetObject.SetActive(true);
            tapScore.text = "TAP SCORE\n8";
            timeReset.text = "BOMB HAS RESET";
            tapLeft = 0;
            tapRight = 0;
            timeLeft = 9f;
            StartCoroutine(CountdownWin());
            timeObject.SetActive(false);
            AmountToTap();
            
        }

        else 

            if (tapLeft != leftCounter | tapRight != rightCounter)

            {

            timeLeft = 0f;
            
        }
        }


    

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = "TIME LEFT\n" + "00:0" + Mathf.RoundToInt(timeLeft);
        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }
    }

    public void LeftButton()
    {
        leftAmount--;

    }

    public void RightButton()
    {
        rightAmount--;
    }


    public void Play()
    {
        SceneManager.LoadScene("TapThat");
    }

    IEnumerator CountdownWin()
    {
            Social.ReportProgress("CgkI5_StjrcWEAIQBg", 100.0f, (bool success) =>
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
        
        tap5Text.SetActive(false);
        tap3Text.SetActive(false);
        pressResetText.SetActive(false);
        timeReset.text = "BOMB HAS RESET";
        yield return new WaitForSeconds(1);
        timeResetObject.SetActive(false);
        timeObject.SetActive(true);
        tapScore.text = "TAP SCORE\n0";
        left.GetComponent<Button>().interactable = true;
        right.GetComponent<Button>().interactable = true;
        AmountToTap();
    }

    IEnumerator CountdownLose()
    {
        tap5Text.SetActive(false);
        tap3Text.SetActive(false);
        pressResetText.SetActive(false);
        timeText.text = "GAME OVER";
        yield return new WaitForSeconds(1);
        disableLeft.SetActive(false);
        disableRight.SetActive(false);
        tapAmountLeft.text = "" + leftAmount;
        tapAmountRight.text = "" + rightAmount;
        timeLeft = 9f;
        left.GetComponent<Button>().interactable = true;
        right.GetComponent<Button>().interactable = true;
        AmountToTap();
    }

    public void AmountToTap()
    {
        leftAmount = 5;
        rightAmount = 3;
    }
}
