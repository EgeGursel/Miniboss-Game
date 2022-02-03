using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject sceneLoader;

    public void Respawn()
    {
        try
        {
            sceneLoader.GetComponent<SceneLoader>().Load(PlayerPrefs.GetString("Scene"));
        }
        catch
        {
            sceneLoader.GetComponent<SceneLoader>().LoadNextScene();
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
}
