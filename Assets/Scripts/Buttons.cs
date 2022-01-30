using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject sceneLoader;
    public void Respawn()
    {
        sceneLoader.GetComponent<SceneLoader>().Load(PlayerPrefs.GetString("Scene"));
    }
}
