using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Start()
    {
        StartCoroutine(StartEssaPorra());
    }
    public void TriggerDialogue() 
    {

        Fade.instance.StartDialogue(dialogue);
    }

    IEnumerator StartEssaPorra() 
    {
        yield return new WaitForSeconds(1f);
        TriggerDialogue();
    }  
}
