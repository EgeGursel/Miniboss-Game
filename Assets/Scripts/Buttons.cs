using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            try
            {
                AudioManager.instance.Stop("theme");
                AudioManager.instance.Stop("love");
            }
            catch
            {
                return;
            }
        }
    }
    public void Respawn()
    {
        try
        {
            SceneLoader.instance.Load(PlayerPrefs.GetString("Scene"));
        }
        catch
        {
            SceneLoader.instance.LoadNextScene();
        }
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("SoulFragments", 0);
        PlayerPrefs.SetFloat("RunSpeed", 1);
        PlayerPrefs.SetFloat("AttackDamage", 1);
        PlayerPrefs.SetFloat("Shield", 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ButtonClick()
    {
        AudioManager.instance.Play("buttonclick");
    }
}
