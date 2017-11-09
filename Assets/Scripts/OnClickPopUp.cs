using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class OnClickPopUp : MonoBehaviour
{

    public GameObject popUp; // prefab to instantiate
    public Canvas canvas; // father canvas
    public Vector2 position;
    public int numberPop;
    public GameObject numberpopUp;


    public void PopUP()

    {

        position.x = UnityEngine.Random.Range(0, Screen.width);
        position.y = UnityEngine.Random.Range(0, Screen.height);
        GameObject clone = (GameObject)Instantiate(popUp, position, Quaternion.identity);
        //this creates the list for colors
        var colors = new List<UnityEngine.Color>();
        //this is the list it takes from
        colors.Add(new UnityEngine.Color(0, 191, 255));
        colors.Add(new UnityEngine.Color(255, 0, 255));
        colors.Add(new UnityEngine.Color(255, 255, 0));
        clone.GetComponent<Text>().color = colors[UnityEngine.Random.Range(0, colors.Count)];
        //clone.transform.SetAsLastSibling();
        clone.transform.SetParent(canvas.transform);

        Destroy(clone, 0.2f);
    }

    public void PopUpNumber()
    {

        position.x = UnityEngine.Random.Range(0, Screen.width);
        position.y = UnityEngine.Random.Range(0, Screen.height);
        GameObject cloneInt = (GameObject)Instantiate(numberpopUp, position, Quaternion.identity);
        //this creates the list for colors
        var colors = new List<UnityEngine.Color>();
        //this is the list it takes from
        colors.Add(new UnityEngine.Color(0, 191, 255));
        colors.Add(new UnityEngine.Color(255, 0, 255));
        colors.Add(new UnityEngine.Color(255, 255, 0));
        cloneInt.GetComponent<Text>().color = colors[UnityEngine.Random.Range(0, colors.Count)];
        cloneInt.transform.SetAsLastSibling();
        cloneInt.transform.SetParent(canvas.transform);
        Destroy(cloneInt, 0.5f);
    }

}
