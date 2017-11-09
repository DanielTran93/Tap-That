using UnityEngine;
using System.Collections;

public class ClickSound : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;

    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
