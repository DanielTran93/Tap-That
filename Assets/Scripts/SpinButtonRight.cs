using UnityEngine;
using System.Collections;

public class SpinButtonRight : MonoBehaviour {

    // Use this for initialization
    void Update()
    {
        transform.Rotate(0, 0, Random.Range(0, -360) * Time.deltaTime / 2);
    }

}
