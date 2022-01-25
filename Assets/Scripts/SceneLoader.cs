using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public void Load(string sceneName)
    {
        StartCoroutine(Loader(sceneName));
    }
    IEnumerator Loader(string sceneName)
    {
        transition.SetTrigger("SceneChange");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
