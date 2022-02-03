using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject upgrades;
    private void Start()
    {
        upgrades.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("RunSpeed");
        upgrades.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("AttackDamage");
        upgrades.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "X" + PlayerPrefs.GetFloat("Shield");
    }
}
