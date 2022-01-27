using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextManager : MonoBehaviour
{
    public Text infoTextOne;
    public string textOne;
    public Text infoTextTwo;
    // Start is called before the first frame update
    void Start()
    {
        infoTextOne.text = textOne;
    }

    public void SetSecondText(string textTwo)
    {
        infoTextTwo.text = textTwo;
    }
}
