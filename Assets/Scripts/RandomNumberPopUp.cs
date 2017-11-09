using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomNumberPopUp : MonoBehaviour {

    public int number;
    public Text numberText;
	// Use this for initialization

	void Start ()
    {
        number = UnityEngine.Random.Range(1, 30);
        numberText.text = "" + number;

    }
	

}
