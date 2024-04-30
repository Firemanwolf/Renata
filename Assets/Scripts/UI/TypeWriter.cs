using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float typewriterSpeed = 50f;
    public bool textfinished;
    public bool canceltyping;

    public bool finishedTyping;

    public static TypeWriter instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        textfinished = true;
        canceltyping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !textfinished)
        {
            canceltyping = !canceltyping;
        }
    }

    public void ShowDialogue(DialogueObject dialogueObject, TextMeshProUGUI dialogueBox)
    {

        dialogueBox.gameObject.SetActive(true);
        finishedTyping = false;
        StartCoroutine(StepThroughDialogue(dialogueObject,dialogueBox));
    }

    private  IEnumerator StepThroughDialogue(DialogueObject dilaogueObject,TextMeshProUGUI textLabel)
    {
        for (int i = 0; i < dilaogueObject.dialogueText.Length; i++)
        {
            string dialogue = dilaogueObject.dialogueText[i];
            yield return Run(dialogue, textLabel);
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && textfinished);
            if (i == dilaogueObject.dialogueText.Length - 1) 
            {
                finishedTyping = true;
                break;
            }
        }
    }



    public Coroutine Run(string textToType, TextMeshProUGUI textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TextMeshProUGUI textLabel)
    {
        textfinished = false;
        textLabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;
        while (!canceltyping && charIndex < textToType.Length)
        {
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            if (charIndex == textToType.Length) break;
            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }
        textfinished = true;
        textLabel.text = textToType;
        canceltyping = false;
    }
}
