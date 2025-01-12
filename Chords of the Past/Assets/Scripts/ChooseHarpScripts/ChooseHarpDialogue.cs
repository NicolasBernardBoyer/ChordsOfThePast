using UnityEngine;
using TMPro;
using System.Collections;

//tysm to this tutorial for the massive help https://www.youtube.com/watch?v=8oTYabhj248!!!

public class ChooseHarpDialogue : MonoBehaviour
{
    static ChooseHarpDialogue instance; //singleton :3
    
    [SerializeField] public TextMeshProUGUI dialogueText;

    public string[] dialogueLines;

    [SerializeField] public float textShowSpeed;

    private int index;


    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    //creating coroutine under this
    IEnumerator TypeLine()
    {
        //Type each character 1 by 1
        foreach (char c in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textShowSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogueLines.Length - 1) //if the index is out of the line
        {
            index = Random.Range(1, 3);
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == dialogueLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index];
            }

        } */
    }

}
