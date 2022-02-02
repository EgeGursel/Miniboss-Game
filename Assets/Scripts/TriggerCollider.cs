using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    private Animator animator;
    public GameObject activateObject;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activateObject.SetActive(true);
            StartCoroutine(Cutscene("Cutscene 1"));
        }
    }
    IEnumerator Cutscene(string boolName)
    {
        animator.SetBool(boolName, true);
        yield return new WaitForSeconds(1.4f);
        animator.SetBool(boolName, false);
    }
}
