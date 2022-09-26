using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fade : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI text;
    
    public static Fade instance;

    public AudioClip fightSceneAudio;
    public AudioClip writeAudio;
    public AudioClip endDialogueAudio;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) 
    {
        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence( string sentence) 
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            text.text += letter;
            if (writeAudio != null)
                AudioManager.instance.PlaySound(writeAudio);
            yield return new WaitForSeconds(0.1f);
        }
        if (endDialogueAudio != null)
            AudioManager.instance.PlaySound(endDialogueAudio);
        yield return new WaitForSeconds(3f);
        LoadManager.instance.LoadScene(2);
        AudioManager.instance.FadeMusic(fightSceneAudio);
    }

}
