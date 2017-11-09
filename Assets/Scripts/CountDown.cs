using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    public GameObject gameController;
    public Text countdownText;
    public GameObject score;
    public GameObject timeLeft;
    public GameObject countDown;
    public AudioSource audioSource;
    public Transform leftButton;
    public Transform rightButton;
    public Transform resetButton;
    public Text leftAmountToTapText;
    public Text rightAmountToTapText;
    public int leftAmountToTap;
    public int rightAmountToTap;

    void Start()
    {
        //starts countdown 
        leftButton.GetComponent<Button>().interactable = false;
        rightButton.GetComponent<Button>().interactable = false;
        resetButton.GetComponent<Button>().interactable = false;
        StartCoroutine(Countdown());
    }

    void Update()
    {
        RandomAmountToTap();
        leftAmountToTapText.text = "" + leftAmountToTap;
        rightAmountToTapText.text = "" + rightAmountToTap;
    }
    IEnumerator Countdown()
    {

        countdownText.text = " ACTIVATE \n3";
        yield return new WaitForSeconds(1);
        countdownText.text = " ACTIVATE \n2";
        yield return new WaitForSeconds(1);
        countdownText.text = " ACTIVATE \n1";
        yield return new WaitForSeconds(1);
        countdownText.text = "BOMB \nACTIVATED";
        yield return new WaitForSeconds(1);
        gameController.SetActive(true);
        score.SetActive(true);
        timeLeft.SetActive(true);
        audioSource.mute = false;
        leftButton.GetComponent<Button>().interactable = true;
        rightButton.GetComponent<Button>().interactable = true;
        resetButton.GetComponent<Button>().interactable = true;
        countDown.SetActive(false);
    }

    public void RandomAmountToTap()
    {
        leftAmountToTap = UnityEngine.Random.Range(5, 65);
        rightAmountToTap = UnityEngine.Random.Range(5, 65);
    }
}
