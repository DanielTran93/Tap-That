using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HowToPlayStart : MonoBehaviour {

    public void StartLevel()
    {
        SceneManager.LoadScene("TapThat");
    }
}
