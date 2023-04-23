using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    void Start(){
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        Debug.Log("Starting Dialogue " + dialogue.name);

        sentences.Clear();

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);

    }
    void EndDialogue(){
        Debug.Log("The end");
    }
}
