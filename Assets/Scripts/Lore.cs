using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lore : MonoBehaviour
{
    public string[] strings;
    private Queue<string> sentences;
    private Text text;
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("theme");
        text = GetComponent<Text>();
        sentences = new Queue<string>();
        StartDialogue();
    }
    public void StartDialogue()
    {
        foreach (string sentence in strings)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        continueButton.gameObject.SetActive(false);
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        continueButton.gameObject.SetActive(true);
    }
    void EndDialogue()
    {
        SceneLoader.instance.LoadNextScene();
    }
}
