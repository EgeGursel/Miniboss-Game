using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public static SceneLoader instance;
    public float transitionTime;

    private void Awake()
    {
        instance = this;
    }
    public void Load(string sceneName)
    {
        StartCoroutine(LoaderByName(sceneName));
    }
    public void LoadNextScene()
    {
        StartCoroutine(LoaderByIndex(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoaderByName(string sceneName)
    {
        transition.SetTrigger("SceneChange");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoaderByIndex(int sceneIndex)
    {
        transition.SetTrigger("SceneChange");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
