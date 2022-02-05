using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMogus : MonoBehaviour
{
    private Animator animator;
    public ParticleSystem lovePS;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("love");
            Instantiate(lovePS);
            StartCoroutine(Cutscene());
        }
    }
    IEnumerator Cutscene()
    {
        animator.SetTrigger("Cutscene 2");
        yield return new WaitForSeconds(4f);
        SceneLoader.instance.Load("MainMenu");
    }
}
