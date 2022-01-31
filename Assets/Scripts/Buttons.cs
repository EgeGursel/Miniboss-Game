using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
    public void Quit()
    {
        Application.Quit();
    }
}
