using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Souls : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        UpdateVisuals();
    }
    public void AddSouls(int addedSouls)
    {
        PlayerPrefs.SetInt("SoulFragments", (PlayerPrefs.GetInt("SoulFragments") + addedSouls));
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        text.text = PlayerPrefs.GetInt("SoulFragments").ToString();
    }
}
