using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMogus : MonoBehaviour
{
    private Animator animator;
    public ParticleSystem lovePS;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.instance.isOpen = true;
            AudioManager.instance.Play("love");
            Instantiate(lovePS, transform.position, transform.rotation);
            StartCoroutine(Cutscene2());
        }
    }
    IEnumerator Cutscene2()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Cutscene 2", true);
        yield return new WaitForSeconds(4f);
        SceneLoader.instance.Load("WinScene");
    }
}
