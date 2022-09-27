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

    public int gameScene;

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
        int count = 0;
        text.text = "";
        foreach (char letter in sentence.ToCharArray()) 
        {
            text.text += letter;
            count++;
            if (writeAudio != null && count == 3) {
                AudioManager.instance.PlaySound(writeAudio);
                count = 0;
            }
            yield return new WaitForSeconds(0.05f);
        }
        if (endDialogueAudio != null)
            AudioManager.instance.PlaySound(endDialogueAudio);
        yield return new WaitForSeconds(3f);
        LoadManager.instance.LoadScene(gameScene);
        AudioManager.instance.FadeMusic(fightSceneAudio);
    }

}
