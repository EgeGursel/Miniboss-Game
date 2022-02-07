using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public bool isOpen = false;
	public static DialogueManager instance;
	private Text nameText;
	private Button continueButton;
	private Text dialogueText;
	private Animator animator;
	private Queue<string> sentences;

    private void Awake()
    {
        instance = this;
    }
    void Start()
	{
		nameText = transform.GetChild(0).GetComponent<Text>();
		dialogueText = transform.GetChild(1).GetComponent<Text>();
		continueButton = transform.GetChild(2).GetComponent<Button>();
		animator = GetComponent<Animator>();
		sentences = new Queue<string>();
	}
	public void StartDialogue(Dialogue dialogue)
	{
		isOpen = true;
		animator.SetBool("IsOpen", true);
		nameText.text = dialogue.name;
		sentences.Clear();
		foreach (string sentence in dialogue.sentences)
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
		dialogueText.text = "";
		AudioManager.instance.Play("talking");
		continueButton.gameObject.SetActive(false);
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.03f);
		}
		AudioManager.instance.Stop("talking");
		continueButton.gameObject.SetActive(true);
		continueButton.interactable = true;
	}
	void EndDialogue()
	{
		continueButton.interactable = false;
		isOpen = false;
		animator.SetBool("IsOpen", false);

    }
}
