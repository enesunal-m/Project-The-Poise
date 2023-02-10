using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string[] dialogueLines;
    [SerializeField] float textSpeed;

    private int index;
    void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(text.text == dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = dialogueLines[index];
            }
        }
        
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in dialogueLines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            CloseDialogueBox();
        }
    }
    void CloseDialogueBox()
    {
        this.gameObject.SetActive(false);
    }
}
