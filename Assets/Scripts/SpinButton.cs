using UnityEngine;
using System.Collections;

public class SpinButton : MonoBehaviour {


    void Update ()
    {
        transform.Rotate(0, 0, Random.Range(0,360) * Time.deltaTime / 2);
    }

    public void Spin()
    {
        transform.Rotate(0, 0, Random.Range(0, 350));
    }
}
