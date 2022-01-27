using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBarManager : MonoBehaviour
{
    public static InfoBarManager instance;
    private void Awake()
    {
        instance = this;
    }

    public Text infoBarText1;
    public string text1;
    public Text infoBarText2;
    public Animator infoBarAnimator;

    // Start is called before the first frame update
    void Start()
    {
        infoBarText1.text = text1;
    }

    public void SendInfoBar(string varText)
    {
        infoBarText2.text = varText;
        StartCoroutine(BarWait());
    }

    public IEnumerator BarWait()
    {
        yield return new WaitForSeconds(3f);
        infoBarAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
