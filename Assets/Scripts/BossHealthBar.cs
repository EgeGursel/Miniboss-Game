using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Animator bossHBAnimator;
    public GameObject sceneLoader;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;

        if (slider.value <= 0)
        {
            StartCoroutine(BossDied());
        }
    }

    IEnumerator BossDied()
    {
        bossHBAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        sceneLoader.GetComponent<SceneLoader>().Load("DeathScene 1");
    }
}

